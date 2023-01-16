using Octokit;

namespace MetricHunter.Application.Repositories;

public interface IRepositoryAppService
{
    Task<Repository[]> ReadRepositoriesAsync(string path, CancellationToken cancellationToken = default);

    Task WriteRepositoriesAsync(IEnumerable<Repository> repositories, string path,
        CancellationToken cancellationToken = default);
}