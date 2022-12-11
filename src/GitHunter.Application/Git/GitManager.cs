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
    private static readonly GitHubClient Client = new(new ProductHeaderValue("GitHunter"));

    public GitManager(ILogger<GitManager> logger, IProcessManager processManager)
    {
        _logger = logger;
        _processManager = processManager;
    }

    private static void Initialize(Credentials? credentials = null)
    {
        if (credentials != null)
        {
            Client.Credentials = credentials;
        }
    }

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
                var files = Directory.GetFiles(repositoryPath, "*", SearchOption.AllDirectories).Select(f=>new FileInfo(f));
                
                foreach (var file in files)
                {
                    file.Delete();
                }
                
                var directories = Directory.GetDirectories(repositoryPath, "*", SearchOption.AllDirectories).Select(f=>new DirectoryInfo(f));
                
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

    public async Task<GitOutput> GetRepositories(GitInput input, Action rateLimitCallBack2)
    {
        CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        var rateLimitCallBack = () => { cancellationTokenSource.Cancel(); };

        rateLimitCallBack += rateLimitCallBack2;


        var inputRanges = Enumerable.Range(1, input.Count)
            .GroupBy(x => x / 1000)
            .Select(x => x.Select(i => i % 1000).Where(i => i > 1).ToList())
            .ToList();

        // inputRanges[0].RemoveAt(0);

        var results = new List<SearchRepositoryResult>();

        var range = Range.LessThanOrEquals(int.MaxValue);

        foreach (var inputRange in inputRanges)
        {
            var firstPage = await TaskRun(CreateTask(input, 1, range), rateLimitCallBack,
                cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                break;

            var tasks = new List<Task<SearchRepositoryResult?>>();

            foreach (var index in inputRange.Take(firstPage!.TotalCount / 100))
            {
                tasks.Add(CreateTask(input, index,
                    range));
            }

            var inputResults = await Task.WhenAll(tasks.Select(x => TaskRun(x, rateLimitCallBack, cancellationToken)));
            results.AddRange(inputResults.Append(firstPage).Where(x => x != null)!);

            if (results.Any(x => x.IncompleteResults))
                break;

            if (cancellationToken.IsCancellationRequested)
                break;

            range = input.Order == SortDirection.Descending
                ? Range.LessThanOrEquals(results.Min(x => x.Items.Min(repository => repository.StargazersCount)))
                : Range.GreaterThanOrEquals(results.Max(x => x.Items.Max(repository => repository.StargazersCount)));
        }

        return new GitOutput(results.SelectMany(t => t.Items).ToList(), results[0].TotalCount,
            results.Any(x => x.IncompleteResults));
    }

    // TODO: Refactor this, get error repositories
    public async Task<GitOutput> TryGetRepositories(GitInput input, Action rateLimitCallBack2)
    {
        CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        var rateLimitCallBack = () => { cancellationTokenSource.Cancel(); };

        rateLimitCallBack += rateLimitCallBack2;

        var tasks = _errorSearchRepositoriesRequests.Values.Select(value =>
            TaskRun(Client.Search.SearchRepo(value), rateLimitCallBack, cancellationToken)).ToList();

        _errorSearchRepositoriesRequests.Clear();

        var inputResults = await Task.WhenAll(tasks);
        inputResults = inputResults.Where(i => i != null).ToArray();

        return new GitOutput(inputResults.SelectMany(t => t!.Items).ToList(),
            inputResults[0]!.TotalCount,
            inputResults.Any(x => x is { IncompleteResults: true }));
    }

    private async Task<SearchRepositoryResult?> TaskRun(Task<SearchRepositoryResult?> task, Action rateLimitCallBack,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return null;

        SearchRepositoryResult? result;

        try
        {
            result = await task;
            _errorSearchRepositoriesRequests.Remove(task);
        }
        catch (Exception e)
        {
            if (cancellationToken.IsCancellationRequested)
                return null;
            if (e.Message.Contains("API rate limit exceeded"))
            {
                rateLimitCallBack();
            }

            result = null;
        }

        return result;
    }

    private readonly Dictionary<Task, SearchRepositoriesRequest> _errorSearchRepositoriesRequests = new();

    private Task<SearchRepositoryResult?> CreateTask(GitInput input, int page, Range stars)
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
        var task = Client.Search.SearchRepo(request);
        _errorSearchRepositoriesRequests.Add(task, request);
        return task;
    }

    public void Initialize(string username, string password)
    {
        Initialize(new Credentials(username, password));
    }

    public void Initialize(string token)
    {
        Initialize(new Credentials(token));
    }
}