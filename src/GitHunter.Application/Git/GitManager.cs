using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;
using Range = Octokit.Range;

namespace GitHunter.Application.Git;

public class GitManager : IGitManager, ITransientDependency
{
    private readonly ILogger<GitManager> _logger;

    private readonly List<List<SearchRepositoriesRequest>> _waitSearchRepositoriesRequests = new();

    private readonly ConcurrentDictionary<SearchRepositoriesRequest, Exception?> _errorSearchRepositoriesRequests =
        new();

    private readonly ConcurrentDictionary<SearchRepositoriesRequest, IReadOnlyList<Repository>> _successRepositories =
        new();

    private Range _range = Range.LessThanOrEquals(int.MaxValue);

    private static readonly GitHubClient Client = new(new ProductHeaderValue("GitHunter"));

    public event EventHandler<SearchRepositoriesRequestErrorEventArgs>? SearchRepositoriesRequestError;
    public event EventHandler<SearchRepositoriesRequestSuccessEventArgs>? SearchRepositoriesRequestSuccess;
    public event EventHandler<SearchRepositoriesRequestFinishedEventArgs>? SearchRepositoriesRequestFinished;

    public GitManager(ILogger<GitManager> logger)
    {
        _logger = logger;
        SearchRepositoriesRequestError += OnSearchRepositoriesRequestError;
        SearchRepositoriesRequestSuccess += OnSearchRepositoriesRequestSuccess;
    }

    private void OnSearchRepositoriesRequestSuccess(object? sender, SearchRepositoriesRequestSuccessEventArgs e)
    {
        _errorSearchRepositoriesRequests.TryRemove(e.SearchRepositoriesRequest, out _);
        _successRepositories.TryAdd(e.SearchRepositoriesRequest, e.Repositories);
    }

    private void OnSearchRepositoriesRequestError(object? sender, SearchRepositoriesRequestErrorEventArgs e)
    {
        _logger.LogError(e.Exception, "Error while searching repositories");
        _errorSearchRepositoriesRequests.TryAdd(e.SearchRepositoriesRequest, e.Exception);
    }

    private static void Initialize(Credentials? credentials = null)
    {
        if (credentials != null)
        {
            Client.Credentials = credentials;
        }
    }
    
    public void Clear()
    {
        _waitSearchRepositoriesRequests.Clear();
        _errorSearchRepositoriesRequests.Clear();
        _successRepositories.Clear();
        _range = Range.LessThanOrEquals(int.MaxValue);
    }

    public IReadOnlyList<Repository> GetAllSuccessRepositories =>
        _successRepositories.Values.SelectMany(x => x).ToList();


    public async Task<GitOutput> GetRepositories(GitInput input)
    {
        CancellationTokenSource cancellationTokenSource = new();

        var inputRanges = GetInputRanges(input.Count, out var lastPerPage);

        var results = new List<SearchRepositoryResult>();

        _range = Range.LessThanOrEquals(int.MaxValue);

        var requests = inputRanges.Select(x => x.Select(x2 => CreateRequest(input, x2, _range)).ToList()).ToList();
        requests.Last().Last().PerPage = lastPerPage;

        _waitSearchRepositoriesRequests.AddRange(requests);

        foreach (var request in requests)
        {
            await AddRequestResult(request, cancellationTokenSource, results);
            _waitSearchRepositoriesRequests.Remove(request);
            if (cancellationTokenSource.IsCancellationRequested)
                break;
        }

        var resultItems = results.SelectMany(x => x.Items).ToList();
        var totalCount = results.Any() ? results.Max(x => x.TotalCount) : 0;
        OnSearchRepositoriesRequestFinished(new SearchRepositoriesRequestFinishedEventArgs(
            resultItems, _errorSearchRepositoriesRequests.Keys.ToList(), _waitSearchRepositoriesRequests));
        return new GitOutput(resultItems, totalCount,
            results.Any(x => x.IncompleteResults));
    }

    private async Task AddRequestResult(List<SearchRepositoriesRequest> requests,
        CancellationTokenSource cancellationTokenSource, List<SearchRepositoryResult> results)
    {
        var tasks = requests.Select(x => TaskRun(x, cancellationTokenSource)).ToList();
        var results2 = await Task.WhenAll(tasks);
        var nonNullResults = results2.Where(x => x != null).Select(r => r!).ToList();
        results.AddRange(nonNullResults);
        if (nonNullResults.Any())
        {
            _range = requests[0].Order == SortDirection.Descending
                ? Range.LessThanOrEquals(results.Min(x => x.Items.Min(repository => repository.StargazersCount)))
                : Range.GreaterThanOrEquals(results.Max(x => x.Items.Max(repository => repository.StargazersCount)));
        }
    }

    private static List<List<int>> GetInputRanges(int count, out int lastPerPage)
    {
        lastPerPage = count % 100;
        count /= 100;
        return Enumerable.Range(1, count + 1)
            .GroupBy(x => x / 11)
            .Select(x => x.Select(i => i % 11).Where(i => i > 0).ToList())
            .ToList();
    }

    public async Task<GitOutput> RetryFailedRequest()
    {
        CancellationTokenSource cancellationTokenSource = new();

        var results = new List<SearchRepositoryResult>();

        var requests = _errorSearchRepositoriesRequests.Keys.ToList();

        await AddRequestResult(requests, cancellationTokenSource, results);

        var waitRequests = _waitSearchRepositoriesRequests.Select(x => x).ToList();

        foreach (var waitSearchRepositoriesRequest in waitRequests)
        {
            if (cancellationTokenSource.IsCancellationRequested)
                break;
            await AddRequestResult(waitSearchRepositoriesRequest, cancellationTokenSource, results);
            _waitSearchRepositoriesRequests.Remove(waitSearchRepositoriesRequest);
        }

        var resultItems = results.Where(r => r is { Items: { } }).SelectMany(x => x.Items).ToList();
        var totalCount = results.Count == 0 ? 0 : results.Max(x => x.TotalCount);
        OnSearchRepositoriesRequestFinished(new SearchRepositoriesRequestFinishedEventArgs(
            resultItems, _errorSearchRepositoriesRequests.Keys.ToList(), _waitSearchRepositoriesRequests));

        return new GitOutput(resultItems, totalCount,
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

    protected virtual void OnSearchRepositoriesRequestFinished(SearchRepositoriesRequestFinishedEventArgs e)
    {
        SearchRepositoriesRequestFinished?.Invoke(this, e);
    }
}