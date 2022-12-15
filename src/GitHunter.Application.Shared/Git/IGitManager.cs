using Octokit;

namespace GitHunter.Application.Git;

public interface IGitManager
{
    /// <summary>
    ///     User login
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    void Initialize(string username, string password);

    /// <summary>
    ///     User login
    /// </summary>
    /// <param name="token"></param>
    void Initialize(string token);

    /// <summary>
    ///     Lists repository information from github by input
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<GitOutput> GetRepositories(GitInput input);

    /// <summary>
    ///     Reruns failed requests
    /// </summary>
    /// <param name="failedRequests"></param>
    /// <returns></returns>
    Task<GitOutput> RetryFailedRequest(HashSet<SearchRepositoriesRequest> failedRequests);

    /// <summary>
    ///     This event is triggered when a request fails.
    /// </summary>
    event EventHandler<RequestErrorEventArgs>? SearchRepositoriesRequestError;

    /// <summary>
    ///     This event is triggered when a request succeeds.
    /// </summary>
    event EventHandler<RequestSuccessEventArgs>? SearchRepositoriesRequestSuccess;

    /// <summary>
    ///     This event is triggered when rate limit is reached.
    /// </summary>
    event EventHandler<RateLimitExceededEventArgs> RateLimitExceeded;

    /// <summary>
    ///     This event is triggered when there is an unknown error in a request.
    /// </summary>
    event EventHandler<ExceptionEventArgs>? OnException;
}