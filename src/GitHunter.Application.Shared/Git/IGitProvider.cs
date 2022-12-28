using Octokit;

namespace GitHunter.Application.Git;

public interface IGitProvider
{
    Task<bool> CloneRepository(Repository repository, string clonePath = "", CancellationToken token = default);
    event EventHandler<CloneRepositoryErrorEventArgs>? CloneRepositoryError;
    event EventHandler<CloneRepositorySuccessEventArgs>? CloneRepositorySuccess;

    Task<bool> DeleteLocalRepository(Repository repository, CancellationToken token = default);
    Task<bool> DeleteLocalRepository(string path, CancellationToken token = default);
}