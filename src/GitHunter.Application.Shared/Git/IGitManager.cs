using Octokit;

namespace GitHunter.Application.Git;

public interface IGitManager
{
    void Initialize(string username, string password);
    void Initialize(string token);
    Task<GitOutput> GetRepositories(GitInput input);
    Task<GitOutput> RetryFailedRequest();
    Task<bool> CloneRepository(Repository repository, CancellationToken token = default);

    event EventHandler<SearchRepositoriesRequestErrorEventArgs>? SearchRepositoriesRequestError;
    event EventHandler<SearchRepositoriesRequestSuccessEventArgs>? SearchRepositoriesRequestSuccess;
}