using Octokit;

namespace GitHunter.Application.Git;

public interface IGitProvider
{
    void Initialize(string username, string password);
    void Initialize(string token);
    Task<bool> CloneRepository(Repository repository, CancellationToken token = default);
    event EventHandler<CloneRepositoryErrorEventArgs>? CloneRepositoryError;
    event EventHandler<CloneRepositorySuccessEventArgs>? CloneRepositorySuccess;

    Task<bool> DeleteLocalRepository(Repository repository, CancellationToken token = default);
    Task<bool> DeleteLocalRepository(string path, CancellationToken token = default);
}