using MetricHunter.Core.Paths;
using Octokit;

namespace MetricHunter.Application.Repositories;

public interface IRepositoryAppService
{
    Task<Repository[]> ReadRepositories(string path);

    Task WriteRepositories(IEnumerable<Repository> repositories, string path);
    Task<List<Repository>> GetRepositoriesFromPaths(FilePath[] infoFiles);
}