using Octokit;

namespace GitHunter.Application.Git;

public interface IGitManager
{
    IReadOnlyList<Repository> GetAllSuccessRepositories { get; }
    void Initialize(string username, string password);
    void Initialize(string token);
    Task<GitOutput> GetRepositories(GitInput input);
    Task<GitOutput> RetryFailedRequest();
    void Clear();

    event EventHandler<SearchRepositoriesRequestErrorEventArgs>? SearchRepositoriesRequestError;
    event EventHandler<SearchRepositoriesRequestSuccessEventArgs>? SearchRepositoriesRequestSuccess;
    event EventHandler<SearchRepositoriesRequestFinishedEventArgs>? SearchRepositoriesRequestFinished;
    event EventHandler<RateLimitExceededEventArgs> RateLimitExceeded;

    // Exception handling
    event EventHandler<ExceptionEventArgs>? OnException;
}