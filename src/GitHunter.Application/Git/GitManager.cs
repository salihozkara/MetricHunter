using GitHunter.Core.DependencyProcesses;
using GitHunter.Core.Helpers;
using GitHunter.Core.Processes;
using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;
using Range = Octokit.Range;

namespace GitHunter.Application.Git;

// TODO: GetRepositories and CloneRepositories should be in separate classes
[ProcessDependency<GitProcessDependency>]
public class GitManager : IGitManager, ITransientDependency
{
    private readonly ILogger<GitManager> _logger;
    private readonly IProcessManager _processManager;
    private readonly Dictionary<SearchRepositoriesRequest, Exception?> _errorSearchRepositoriesRequests = new();
    private readonly Dictionary<SearchRepositoriesRequest, IReadOnlyList<Repository>> _successRepositories = new();
    private static readonly GitHubClient Client = new(new ProductHeaderValue("GitHunter"));


    public event EventHandler<SearchRepositoriesRequestErrorEventArgs>? SearchRepositoriesRequestError;
    public event EventHandler<SearchRepositoriesRequestSuccessEventArgs>? SearchRepositoriesRequestSuccess;

    public GitManager(ILogger<GitManager> logger, IProcessManager processManager)
    {
        _logger = logger;
        _processManager = processManager;

        SearchRepositoriesRequestError += OnSearchRepositoriesRequestError;
        SearchRepositoriesRequestSuccess += OnSearchRepositoriesRequestSuccess;
    }

    private void OnSearchRepositoriesRequestSuccess(object? sender, SearchRepositoriesRequestSuccessEventArgs e)
    {
        _errorSearchRepositoriesRequests.Remove(e.SearchRepositoriesRequest);
        _successRepositories.Add(e.SearchRepositoriesRequest, e.Repositories);
    }

    private void OnSearchRepositoriesRequestError(object? sender, SearchRepositoriesRequestErrorEventArgs e)
    {
        _logger.LogError(e.Exception, "Error while searching repositories");
        _errorSearchRepositoriesRequests.Add(e.SearchRepositoriesRequest, e.Exception);
    }

    private static void Initialize(Credentials? credentials = null)
    {
        if (credentials != null)
        {
            Client.Credentials = credentials;
        }
    }

    public IReadOnlyList<Repository> GetAllSuccessRepositories() =>
        _successRepositories.Values.SelectMany(x => x).ToList();

    // TODO: Add path to parameters
    public async Task<bool> CloneRepository(Repository repository, CancellationToken token = default)
    {
        if (token.IsCancellationRequested)
        {
            return false;
        }

        _logger.LogInformation($"Cloning {repository.FullName}...");
        var path = PathHelper.BuildAndCreateFullPath(repository.Language, "Repositories", repository.Owner.Login);

        var repositoryPath = Path.Combine(path, repository.Name);
        if (Directory.Exists(repositoryPath))
        {
            try
            {
                var files = Directory.GetFiles(repositoryPath, "*", SearchOption.AllDirectories)
                    .Select(f => new FileInfo(f));

                foreach (var file in files)
                {
                    file.Delete();
                }

                var directories = Directory.GetDirectories(repositoryPath, "*", SearchOption.AllDirectories)
                    .Select(f => new DirectoryInfo(f));

                foreach (var directory in directories)
                {
                    directory.Delete(true);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to delete {repositoryPath}");
            }
            // var directoryInfo = new DirectoryInfo(repositoryPath);
            // var dirs = directoryInfo.GetDirectories().Where(d => d.Name != ".git").ToList();
            // var dirSize =
            //     dirs.Sum(d => d.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length)) / 1024;
            // if (dirSize < repository.Size)
            // {
            //     _logger.LogInformation(
            //         $"Repository {repository.FullName} already exists, but is smaller than expected. Deleting and cloning again...");
            //     Directory.Delete(repositoryPath, true);
            // }
            // else
            // {
            //     _logger.LogInformation($"Repository {repository.FullName} already exists. Skipping...");
            //     return true;
            // }
        }

        var result = await _processManager.RunAsync("git", $"clone -c core.longpaths=true {repository.CloneUrl}", path);

        if (result.ExitCode == 0)
        {
            _logger.LogInformation($"Cloned {repository.FullName} successfully.");
        }
        else
        {
            _logger.LogError($"Failed to clone {repository.FullName}.");
            token.ThrowIfCancellationRequested();
        }

        return result.ExitCode == 0;
    }

    public async Task<GitOutput> GetRepositories(GitInput input)
    {
        CancellationTokenSource cancellationTokenSource = new();

        var inputRanges = GetInputRanges(input.Count);

        var results = new List<SearchRepositoryResult>();

        var range = Range.LessThanOrEquals(int.MaxValue);

        var requests = new List<SearchRepositoriesRequest>();

        foreach (var inputRange in inputRanges)
        {
            var request = CreateRequest(input, 1, range);
            var firstPage = await TaskRun(request,
                cancellationTokenSource);

            if (cancellationTokenSource.IsCancellationRequested)
                break;

            // foreach (var index in inputRange.Take(firstPage!.TotalCount / 100))
            // {
            //     requests.Add(CreateRequest(input, index, range));
            // }
            requests.AddRange(inputRange.Take(firstPage!.TotalCount / 100)
                .Select(index => CreateRequest(input, index, range)));

            await AddRequestResult(requests, cancellationTokenSource, results, firstPage);

            if (results.Any(x => x.IncompleteResults))
                break;

            if (cancellationTokenSource.IsCancellationRequested)
                break;

            range = input.Order == SortDirection.Descending
                ? Range.LessThanOrEquals(results.Min(x => x.Items.Min(repository => repository.StargazersCount)))
                : Range.GreaterThanOrEquals(results.Max(x => x.Items.Max(repository => repository.StargazersCount)));
        }

        return new GitOutput(results.SelectMany(t => t.Items).ToList(), results[0].TotalCount,
            results.Any(x => x.IncompleteResults));
    }

    private static List<List<int>> GetInputRanges(int count)
    {
        return Enumerable.Range(1, count)
            .GroupBy(x => x / 1000)
            .Select(x => x.Select(i => i % 1000).Where(i => i > 1).ToList())
            .ToList();
    }

    private async Task AddRequestResult(List<SearchRepositoriesRequest> requests,
        CancellationTokenSource cancellationTokenSource,
        List<SearchRepositoryResult> results, SearchRepositoryResult? firstPage)
    {
        var inputResults = await Task.WhenAll(requests.Select(x => TaskRun(x, cancellationTokenSource)));
        results.AddRange(inputResults.Append(firstPage).Where(x => x != null)!);
    }

    public async Task<GitOutput> RetryFailedRequest()
    {
        CancellationTokenSource cancellationTokenSource = new();

        var results = new List<SearchRepositoryResult>();

        var requests = _errorSearchRepositoriesRequests.Keys.ToList();

        _errorSearchRepositoriesRequests.Clear();

        await AddRequestResult(requests, cancellationTokenSource, results,
            null!);

        return new GitOutput(results.SelectMany(t => t.Items).ToList(), results[0].TotalCount,
            results.Any(x => x is { IncompleteResults: true }));
    }

    private async Task<SearchRepositoryResult?> TaskRun(SearchRepositoriesRequest request,
        CancellationTokenSource cancellationTokenSource)
    {
        if (cancellationTokenSource.IsCancellationRequested)
            return null;

        SearchRepositoryResult? result;

        var task = CreateTask(request);
        try
        {
            result = await task;
            if (result == null)
            {
                OnSearchRepositoriesRequestError(
                    new SearchRepositoriesRequestErrorEventArgs(request, cancellationTokenSource, task.Exception));
            }
            else
            {
                OnSearchRepositoriesRequestSuccess(
                    new SearchRepositoriesRequestSuccessEventArgs(request, result.Items));
            }
        }
        catch (Exception e)
        {
            OnSearchRepositoriesRequestError(
                new SearchRepositoriesRequestErrorEventArgs(request, cancellationTokenSource, e));

            result = null;
        }

        return result;
    }


    private Task<SearchRepositoryResult?> CreateTask(SearchRepositoriesRequest request)
    {
        return Client.Search.SearchRepo(request);
    }

    private SearchRepositoriesRequest CreateRequest(GitInput input, int page, Range stars)
    {
        var request = new SearchRepositoriesRequest
        {
            Language = input.Language,
            Topic = input.Topic,
            SortField = RepoSearchSort.Stars,
            Stars = stars,
            Page = page,
            PerPage = input.Count < 100 ? input.Count : 100,
            Order = input.Order
        };
        return request;
    }

    public void Initialize(string username, string password)
    {
        Initialize(new Credentials(username, password));
    }

    public void Initialize(string token)
    {
        Initialize(new Credentials(token));
    }

    protected virtual void OnSearchRepositoriesRequestError(SearchRepositoriesRequestErrorEventArgs e)
    {
        e.CancellationTokenSource.Cancel();
        SearchRepositoriesRequestError?.Invoke(this, e);
    }

    protected virtual void OnSearchRepositoriesRequestSuccess(SearchRepositoriesRequestSuccessEventArgs e)
    {
        SearchRepositoriesRequestSuccess?.Invoke(this, e);
    }
}