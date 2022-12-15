using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;
using Range = Octokit.Range;

namespace GitHunter.Application.Git;

public class OctokitGitManager : IGitManager, ITransientDependency
{
    private static readonly GitHubClient Client = new(new ProductHeaderValue("GitHunter"));
    private readonly ILogger<OctokitGitManager> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OctokitGitManager" /> class.
    /// </summary>
    /// <param name="logger"></param>
    public OctokitGitManager(ILogger<OctokitGitManager> logger)
    {
        _logger = logger;
    }


    /// <summary>
    ///     This event is triggered when a request fails.
    /// </summary>
    public event EventHandler<RequestErrorEventArgs>? SearchRepositoriesRequestError;

    /// <summary>
    ///     This event is triggered when a request succeeds.
    /// </summary>
    public event EventHandler<RequestSuccessEventArgs>? SearchRepositoriesRequestSuccess;

    /// <summary>
    ///     This event is triggered when rate limit is reached.
    /// </summary>
    public event EventHandler<RateLimitExceededEventArgs>? RateLimitExceeded;

    /// <summary>
    ///     This event is triggered when there is an unknown error in a request.
    /// </summary>
    public event EventHandler<ExceptionEventArgs>? OnException;

    /// <summary>
    ///     User login
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public void Initialize(string username, string password)
    {
        Client.Credentials = new Credentials(username, password);
    }

    /// <summary>
    ///     User login
    /// </summary>
    /// <param name="token"></param>
    public void Initialize(string token)
    {
        Client.Credentials = new Credentials(token);
    }

    /// <summary>
    ///     Lists repository information from github by input
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<GitOutput> GetRepositories(GitInput input)
    {
        HashSet<SearchRepositoriesRequest> failedRequests = new();
        var result = await GetRepositories(input, null, failedRequests);
        return new GitOutput(result.Repositories.Take(input.Count).ToList(), result.FailedRequests);
    }

    /// <summary>
    ///     Reruns failed requests
    /// </summary>
    /// <param name="failedRequests"></param>
    /// <returns></returns>
    public async Task<GitOutput> RetryFailedRequest(HashSet<SearchRepositoriesRequest> failedRequests)
    {
        _logger.LogWarning("Retrying failed requests");
        var requests = failedRequests.ToList();
        failedRequests.Clear();
        var results = await RunRequests(requests, failedRequests);
        _logger.LogWarning("Retrying finished");
        return new GitOutput(ConvertToRepositories(results), failedRequests);
    }

    /// <summary>
    ///     Converts search results to repositories
    /// </summary>
    /// <param name="results"></param>
    /// <returns></returns>
    private IReadOnlyList<Repository> ConvertToRepositories(params SearchRepositoryResult?[] results)
    {
        return results.Where(r => r is { Items.Count: > 0 }).SelectMany(r => r!.Items).ToList();
    }

    /// <summary>
    ///     Generates page numbers based on the requested repository number
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private IEnumerable<int> GetPageNumbers(int count)
    {
        var pages = count / 100;
        pages += count % 100 == 0 ? 0 : 1;
        for (var i = 1; i <= pages; i++) yield return i;
    }

    /// <summary>
    ///     Recursively pulls repository information based on input value
    /// </summary>
    /// <param name="input"></param>
    /// <param name="range"></param>
    /// <param name="failedRequests"></param>
    /// <returns></returns>
    private async Task<GitOutput> GetRepositories(GitInput input, Range? range,
        HashSet<SearchRepositoriesRequest> failedRequests)
    {
        var stars = range;
        var repositories = new List<Repository>();
        var requests = GetPageNumbers(input.Count)
            .Select(x => CreateRequest(input, x, stars));

        var results = await RunRequests(requests, failedRequests);
        repositories.AddRange(ConvertToRepositories(results));
        if (repositories.Count >= input.Count)
            return new GitOutput(repositories, failedRequests);

        // Updating range as only the first 1000 search results are available in Github requests
        if (repositories.Any())
            range = input.Order == SortDirection.Descending
                ? Range.LessThanOrEquals(repositories.Min(x => x.StargazersCount))
                : Range.GreaterThanOrEquals(repositories.Max(x => x.StargazersCount));

        var result = await GetRepositories(input, range, failedRequests);
        repositories.AddRange(result.Repositories);
        return new GitOutput(repositories, failedRequests);
    }

    /// <summary>
    ///     Runs requests
    /// </summary>
    /// <param name="requests"></param>
    /// <param name="failedRequests"></param>
    /// <returns></returns>
    private async Task<SearchRepositoryResult?[]> RunRequests(IEnumerable<SearchRepositoriesRequest> requests,
        HashSet<SearchRepositoriesRequest> failedRequests)
    {
        var tasks = requests.Select(r => RunRequest(r, failedRequests));

        await RateLimitWait();

        return await Task.WhenAll(tasks);
    }

    /// <summary>
    ///     Runs a single request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="failedRequests"></param>
    /// <returns></returns>
    private async Task<SearchRepositoryResult?> RunRequest(SearchRepositoriesRequest request,
        HashSet<SearchRepositoriesRequest> failedRequests)
    {
        failedRequests.Add(request);
        SearchRepositoryResult? result = null;
        try
        {
            result = await Client.Search.SearchRepo(request);
        }
        catch (RateLimitExceededException)
        {
            await RateLimitWait();
            try
            {
                _logger.LogWarning("Retrying request");
                result = await Client.Search.SearchRepo(request);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Retrying failed");
                SearchRepositoriesRequestError?.Invoke(this, new RequestErrorEventArgs(request, exception));
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Request failed");
            SearchRepositoriesRequestError?.Invoke(this, new RequestErrorEventArgs(request, e));
        }

        if (result is not null)
        {
            failedRequests.Remove(request);
            SearchRepositoriesRequestSuccess?.Invoke(this, new RequestSuccessEventArgs(request, result));
        }

        return result;
    }

    /// <summary>
    ///     Waits for rate limit to reset or client handle to be available
    /// </summary>
    private async Task RateLimitWait()
    {
        try
        {
            var rateLimits = await Client.RateLimit.GetRateLimits();
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
                    if (waitTime.TotalSeconds > 0)
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