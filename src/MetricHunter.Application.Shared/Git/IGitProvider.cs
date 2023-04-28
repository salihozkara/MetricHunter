using Octokit;

namespace MetricHunter.Application.Git;

public interface IGitProvider
{
    Task<bool> CloneRepositoryAsync(Repository repository, string cloneBaseDirectoryPath = "", string branchName = "",
        CancellationToken cancellationToken = default);

    event EventHandler<CloneRepositoryErrorEventArgs>? CloneRepositoryError;
    event EventHandler<CloneRepositorySuccessEventArgs>? CloneRepositorySuccess;

    Task<bool> DeleteLocalRepositoryAsync(Repository repository, string cloneBaseDirectoryPath = "",
        string branchName = "", CancellationToken token = default);

    Task<bool> DeleteLocalRepositoryAsync(string path, CancellationToken token = default);
}