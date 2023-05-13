namespace MetricHunter.Application.Repositories;

public interface IRepositoryAppService
{
    Task<RepositoryWithBranchNameDto[]> ReadRepositoriesAsync(string path, CancellationToken cancellationToken = default);
    
    Task<RepositoryWithBranchNameDto?> ReadRepositoryAsync(string path, CancellationToken cancellationToken = default);

    Task WriteRepositoriesAsync(IEnumerable<RepositoryWithBranchNameDto> repositories, string path,
        CancellationToken cancellationToken = default);
}