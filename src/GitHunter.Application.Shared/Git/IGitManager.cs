using Octokit;

namespace GitHunter.Application.Git;

public interface IGitManager
{
    void Initialize(string username, string password);
    void Initialize(string token);
    Task<GitOutput> GetRepositories(GitInput input, Action rateLimitCallBack);
    Task<GitOutput> TryGetRepositories(GitInput input, Action rateLimitCallBack);
    Task<bool> CloneRepository(Repository repository, CancellationToken token = default);
}