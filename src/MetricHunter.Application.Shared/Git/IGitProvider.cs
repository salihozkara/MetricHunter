using Octokit;

namespace MetricHunter.Application.Git;

public interface IGitProvider
{
    Task<bool> CloneRepository(Repository repository, string cloneBaseDirectoryPath = "",
        CancellationToken token = default);

    event EventHandler<CloneRepositoryErrorEventArgs>? CloneRepositoryError;
    event EventHandler<CloneRepositorySuccessEventArgs>? CloneRepositorySuccess;

    Task<bool> DeleteLocalRepository(Repository repository, string cloneBaseDirectoryPath = "",
        CancellationToken token = default);

    Task<bool> DeleteLocalRepository(string path, CancellationToken token = default);
}