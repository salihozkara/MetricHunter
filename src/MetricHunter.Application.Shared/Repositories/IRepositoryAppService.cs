using Octokit;

namespace MetricHunter.Application.Repositories;

public interface IRepositoryAppService
{
    Task<Repository[]> ReadRepositoriesAsync(string path);

    Task WriteRepositoriesAsync(IEnumerable<Repository> repositories, string path);
}