﻿using System.Reflection;
using MetricHunter.Core.Languages;
using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;
using Language = Octokit.Language;
using Range = Octokit.Range;

namespace MetricHunter.Application.Git;

public class OctokitGitManager : IGitManager, ITransientDependency
{
    private const int MaxPage = 10;
    private const int PerPage = 100;
    private static readonly GitHubClient Client = new(new ProductHeaderValue("MetricHunter"));
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
    
    private (string owner, string name) GetOwnerAndName(string repositoryFullNameOrUrl)
    {
        if (Uri.TryCreate(repositoryFullNameOrUrl, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
        {
            var split = uriResult.AbsolutePath.Split('/');
            var owner = split[1];
            var name = split[2];
            return (owner, name);
        }
        else
        {
            var split = repositoryFullNameOrUrl.Split('/');
            var owner = split[0];
            var name = split[1];
            return (owner, name);
        }
    }

    public Task<Repository> GetRepositoryAsync(string repositoryFullNameOrUrl, CancellationToken cancellationToken = default)
    {
        
        var (owner, name) = GetOwnerAndName(repositoryFullNameOrUrl);
        return Client.Repository.Get(owner, name);
    }
    
    public Task<IReadOnlyList<Branch>> GetBranchesAsync(string repositoryFullNameOrUrl, CancellationToken cancellationToken = default)
    {
        var (owner, name) = GetOwnerAndName(repositoryFullNameOrUrl);
        return Client.Repository.Branch.GetAll(owner, name).WaitAsync(cancellationToken);
    }

    public Task<IReadOnlyList<Release>> GetReleasesAsync(string repositoryFullNameOrUrl, CancellationToken cancellationToken = default)
    {
        var (owner, name) = GetOwnerAndName(repositoryFullNameOrUrl);
        return Client.Repository.Release.GetAll(owner, name, new ApiOptions()
        {
            PageCount = 1,
            PageSize = 50
        });
    }
    
    public Task<IReadOnlyList<GitHubCommit>> GetCommitsAsync(string repositoryFullNameOrUrl, CancellationToken cancellationToken = default)
    {
        var (owner, name) = GetOwnerAndName(repositoryFullNameOrUrl);
        return Client.Repository.Commit.GetAll(owner, name, new ApiOptions()
        {
            PageCount = 1,
            PageSize = 100
        });
    }
    
    public Task<IReadOnlyList<Repository>> GetRepositoriesByOwnerAsync(string owner, CancellationToken cancellationToken = default)
    {
        return Client.Repository.GetAllForUser(owner, new ApiOptions()
        {
            PageCount = 1,
            PageSize = 100
        });
    }

    /// <summary>
    ///     User login
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public bool Authenticate(string username, string password)
    {
        try
        {
            Client.Credentials = new Credentials(username, password);
            Client.User.Current().GetAwaiter().GetResult();
            return true;
        }
        catch (Exception e)
        {
            Client.Credentials = Credentials.Anonymous;
            _logger.LogError(e, "Error while authenticating");
            return false;
        }
    }

    /// <summary>
    ///     User login
    /// </summary>
    /// <param name="token"></param>
    public bool Authenticate(string token)
    {
        try
        {
            Client.Credentials = new Credentials(token);
            Client.User.Current().GetAwaiter().GetResult();
            return true;
        }
        catch (Exception e)
        {
            Client.Credentials = Credentials.Anonymous;
            _logger.LogError(e, "Error while authenticating");
            return false;
        }
    }

    /// <summary>
    ///     Lists repository information from github by input
    /// </summary>
    /// <param name="input"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<GitOutput> GetRepositoriesAsync(GitInput input, CancellationToken cancellationToken = default)
    {
        List<SearchRepositoriesRequest> failedRequests = new();
        List<Repository> repositories = new();
        Range? stars = null;

        do
        {
            cancellationToken.ThrowIfCancellationRequested();
            var requests = GetPageNumbers(input.Count - repositories.Count).Select(p => CreateRequest(input, p, stars))
                .ToList();
            var results = await RunRequestsAsync(requests, failedRequests, cancellationToken);
            var items = ConvertToRepositories(results);
            repositories.AddRange(items);
            var count = repositories.Count;
            repositories = repositories.DistinctBy(r => r.CloneUrl).ToList();
            var newCount = repositories.Count;
            if (count != newCount)
                _logger.LogWarning("{count} repositories were removed because of duplication", count - newCount);

            if (results.Where(r => r != null).Any(i => i!.Items.Count != PerPage))
            {
                _logger.LogWarning("No more repositories found");
                break;
            }

            // Updating range as only the first 1000 search results are available in Github requests
            if (repositories.Any())
                stars = input.Order == SortDirection.Descending
                    ? Range.LessThanOrEquals(repositories.Min(x => x.StargazersCount))
                    : Range.GreaterThanOrEquals(repositories.Max(x => x.StargazersCount));
        }while(repositories.Count < input.Count && repositories.Count != 0);

        return new GitOutput(repositories.Take(input.Count).ToList(), failedRequests);
    }

    public async Task<bool> IsRepositoryPublicAsync(Repository repository,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var defaultDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        if (repository.CreatedAt < defaultDate || repository.UpdatedAt < defaultDate)
            return false;
        try
        {
            var client = new HttpClient();
            var response = await client.GetAsync(repository.HtmlUrl, cancellationToken);
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to check if repository is public");
            return false;
        }
    }

    /// <summary>
    ///     Reruns failed requests
    /// </summary>
    /// <param name="failedRequests"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<GitOutput> RetryFailedRequestAsync(List<SearchRepositoriesRequest> failedRequests,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogWarning("Retrying failed requests");
        var requests = failedRequests.ToList();
        failedRequests.Clear();
        var results = await RunRequestsAsync(requests, failedRequests, cancellationToken);
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
        if (count <= 0) return ArraySegment<int>.Empty;

        var pages = Math.DivRem(count, PerPage, out var rem);
        if (rem != 0) pages++;

        return Enumerable.Range(0, pages).Select(i => i % MaxPage + 1);
    }

    /// <summary>
    ///     Runs requests
    /// </summary>
    /// <param name="requests"></param>
    /// <param name="failedRequests"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<SearchRepositoryResult?[]> RunRequestsAsync(IEnumerable<SearchRepositoriesRequest> requests,
        List<SearchRepositoriesRequest> failedRequests, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var tasks = requests.Select(r => RunRequestAsync(r, failedRequests, cancellationToken));
        return await Task.WhenAll(tasks).WaitAsync(cancellationToken);
    }

    /// <summary>
    ///     Runs a single request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="failedRequests"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<SearchRepositoryResult?> RunRequestAsync(SearchRepositoriesRequest request,
        ICollection<SearchRepositoriesRequest> failedRequests, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (!failedRequests.Contains(request))
            failedRequests.Add(request);
        SearchRepositoryResult? result = null;
        try
        {
            result = await Client.Search.SearchRepo(request).WaitAsync(cancellationToken);
        }
        catch (RateLimitExceededException)
        {
            await RateLimitWaitAsync(cancellationToken);
            try
            {
                _logger.LogWarning("Retrying request");
                result = await RunRequestAsync(request, failedRequests, cancellationToken);
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
    private async Task RateLimitWaitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var rateLimits = await Client.RateLimit.GetRateLimits().WaitAsync(cancellationToken);
            // wait rate limit
            if (rateLimits.Resources.Search.Remaining == 0)
            {
                var args = new RateLimitExceededEventArgs(rateLimits.Resources.Search.Reset);
                RateLimitExceeded?.Invoke(this, args);
                if (args.Wait)
                {
                    var resetTime = rateLimits.Resources.Search.Reset;
                    var waitTime = resetTime - DateTimeOffset.Now;
                    if (waitTime.TotalSeconds > 0)
                    {
                        _logger.LogWarning("Rate limit exceeded, waiting {0} seconds", waitTime.TotalSeconds);
                        await Task.Delay(waitTime, cancellationToken);
                        _logger.LogWarning("Rate limit reset");
                    }
                }
            }
        }
        catch (Exception e) when (e is not TaskCanceledException)
        {
            _logger.LogError(e, "Rate limit check failed");
            // wait 1 minute
            var args = new ExceptionEventArgs(e);
            OnException?.Invoke(this, args);
            if (args.ThrowException)
                throw;
            if (args.Retry)
                await RateLimitWaitAsync(cancellationToken);
            if (args.Handled)
                return;
            if (args.DefaultExceptionHandling)
            {
                _logger.LogWarning("Waiting 1 minute");
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
                _logger.LogWarning("Wait finished");
            }
        }
    }

    private SearchRepositoriesRequest CreateRequest(GitInput input, int page, Range? stars)
    {
        string? term = null;
        
        if (input.Language.HasValue)
        {
            var languageInfo = input.Language.Value.GetType().GetField(input.Language.Value.ToString())?.GetCustomAttribute<LanguageInfoAttribute>();
            var lang = languageInfo?.ParameterName ?? input.Language.Value.ToString();
            term = $"language:{lang}";
        }

        var request = !term.IsNullOrWhiteSpace() ? new SearchRepositoriesRequest(term) : new SearchRepositoriesRequest();

        request.Topic = input.Topic.IsNullOrWhiteSpace() ? null : input.Topic;
        request.SortField = RepoSearchSort.Stars;
        request.Stars = stars;
        request.Page = page;
        request.PerPage = PerPage;
        request.Order = input.Order;

        
        return request;
    }
}