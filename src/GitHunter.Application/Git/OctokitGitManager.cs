using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;
using Range = Octokit.Range;

namespace GitHunter.Application.Git;

public class OctokitGitManager : IGitManager, ITransientDependency
{
    private readonly GitHubClient _client = new(new ProductHeaderValue("GitHunter"));
    private readonly List<SearchRepositoriesRequest> _failedRequests = new();
    private readonly ILogger<OctokitGitManager> _logger;
    private List<Repository> _repositories = new();


    public OctokitGitManager(ILogger<OctokitGitManager> logger)
    {
        _logger = logger;
        SearchRepositoriesRequestSuccess += OnSearchRepositoriesRequestSuccess;
        SearchRepositoriesRequestError += OnSearchRepositoriesRequestError;
    }


    public event EventHandler<SearchRepositoriesRequestErrorEventArgs>? SearchRepositoriesRequestError;
    public event EventHandler<SearchRepositoriesRequestSuccessEventArgs>? SearchRepositoriesRequestSuccess;
    public event EventHandler<SearchRepositoriesRequestFinishedEventArgs>? SearchRepositoriesRequestFinished;
    public event EventHandler<RateLimitExceededEventArgs>? RateLimitExceeded;
    public event EventHandler<ExceptionEventArgs>? OnException;

    public void Initialize(string username, string password)
    {
        _client.Credentials = new Credentials(username, password);
    }

    public void Initialize(string token)
    {
        _client.Credentials = new Credentials(token);
    }

    public async Task<GitOutput> GetRepositories(GitInput input)
    {
        var result = await GetRepositories(input, null);
        SearchRepositoriesRequestFinished?.Invoke(this,
            new SearchRepositoriesRequestFinishedEventArgs(_repositories, _failedRequests));
        return result;
    }

    public async Task<GitOutput> RetryFailedRequest()
    {
        _logger.LogWarning("Retrying failed requests");
        await RunRequests(_failedRequests);
        _logger.LogWarning("Retrying finished");
        return new GitOutput(_repositories, int.MaxValue, true);
    }

    public IReadOnlyList<Repository> GetAllSuccessRepositories => _repositories;

    public void Clear()
    {
        _repositories.Clear();
    }

    private void OnSearchRepositoriesRequestError(object? sender, SearchRepositoriesRequestErrorEventArgs e)
    {
        _failedRequests.Add(e.SearchRepositoriesRequest);
    }

    private void OnSearchRepositoriesRequestSuccess(object? sender, SearchRepositoriesRequestSuccessEventArgs e)
    {
        _failedRequests.Remove(e.SearchRepositoriesRequest);
        _repositories.AddRange(e.SearchRepositoryResult.Items ?? new List<Repository>());
    }

    private async Task<GitOutput> GetRepositories(GitInput input, Range? range)
    {
        var stars = range;
        var requests = Enumerable.Range(1, 10)
            .Select(x => CreateRequest(input, x, stars));

        await RunRequests(requests);
        if (_repositories.Count >= input.Count)
            return new GitOutput(_repositories.Take(input.Count).ToList(), int.MaxValue, true);

        if (_repositories.Any())
            range = input.Order == SortDirection.Descending
                ? Range.LessThanOrEquals(_repositories.Min(x => x.StargazersCount))
                : Range.GreaterThanOrEquals(_repositories.Max(x => x.StargazersCount));

        return await GetRepositories(input, range);
    }

    private async Task RunRequests(IEnumerable<SearchRepositoriesRequest> requests)
    {
        var tasks = requests.Select(RunRequest);

        await RateLimitWait();

        await Task.WhenAll(tasks);

        _repositories = _repositories.DistinctBy(x => x.CloneUrl).ToList();
    }

    private async Task<SearchRepositoryResult?> RunRequest(SearchRepositoriesRequest x)
    {
        SearchRepositoryResult? result = null;
        try
        {
            result = await _client.Search.SearchRepo(x);
            if (result != null)
                SearchRepositoriesRequestSuccess?.Invoke(this,
                    new SearchRepositoriesRequestSuccessEventArgs(x, result));
        }
        catch (RateLimitExceededException)
        {
            await RateLimitWait();
            try
            {
                _logger.LogWarning("Retrying request");
                result = await _client.Search.SearchRepo(x);
                if (result != null)
                    SearchRepositoriesRequestSuccess?.Invoke(this,
                        new SearchRepositoriesRequestSuccessEventArgs(x, result));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Retrying failed");
                SearchRepositoriesRequestError?.Invoke(this, new SearchRepositoriesRequestErrorEventArgs(x, exception));
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Request failed");
            SearchRepositoriesRequestError?.Invoke(this, new SearchRepositoriesRequestErrorEventArgs(x, e));
            var args = new ExceptionEventArgs(e);
            OnException?.Invoke(this, args);
            if (args.ThrowException)
                throw;
            if (args.Retry) result = await RunRequest(x);
        }

        return result;
    }

    private async Task RateLimitWait()
    {
        try
        {
            var rateLimits = await _client.RateLimit.GetRateLimits();
            // wait rate limit
            if (rateLimits.Resources.Search.Remaining == 0)
            {
                var args = new RateLimitExceededEventArgs(rateLimits.Resources.Search.Reset);
                RateLimitExceeded?.Invoke(this, args);
                if (args.Wait)
                {
                    var resetTime = rateLimits.Resources.Search.Reset;
                    var waitTime = resetTime - DateTimeOffset.Now;
                    _logger.LogWarning("Rate limit exceeded, waiting {0} seconds", waitTime.TotalSeconds);
                    if(waitTime.TotalSeconds > 0)
                        await Task.Delay(waitTime);
                    _logger.LogWarning("Rate limit reset");
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Rate limit check failed");
            // wait 1 minute
            var args = new ExceptionEventArgs(e);
            OnException?.Invoke(this, args);
            if (args.ThrowException)
                throw;
            if (args.Retry)
                await RateLimitWait();
            if (args.Handled)
                return;
            if (args.DefaultExceptionHandling)
            {
                _logger.LogWarning("Waiting 1 minute");
                await Task.Delay(TimeSpan.FromMinutes(1));
                _logger.LogWarning("Wait finished");
            }
        }
    }

    private SearchRepositoriesRequest CreateRequest(GitInput input, int page, Range? stars)
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
}