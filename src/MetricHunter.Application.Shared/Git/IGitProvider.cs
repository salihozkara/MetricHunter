using MetricHunter.Application.Repositories;

namespace MetricHunter.Application.Git;

public interface IGitProvider
{
    Task<bool> CloneRepositoryAsync(RepositoryWithBranchNameDto repository, string cloneBaseDirectoryPath = "",
        CancellationToken cancellationToken = default);

    event EventHandler<CloneRepositoryErrorEventArgs>? CloneRepositoryError;
    event EventHandler<CloneRepositorySuccessEventArgs>? CloneRepositorySuccess;

    Task<bool> DeleteLocalRepositoryAsync(RepositoryWithBranchNameDto repositoryWithBranchNameDto, string cloneBaseDirectoryPath = "", CancellationToken token = default);

    Task<bool> DeleteLocalRepositoryAsync(string path, CancellationToken token = default);
}