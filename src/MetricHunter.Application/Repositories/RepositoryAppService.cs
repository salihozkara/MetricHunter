using MetricHunter.Core.Jsons;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Application.Repositories;

public class RepositoryAppService : IRepositoryAppService, ISingletonDependency
{
    public async Task<Repository[]> ReadRepositoriesAsync(string path)
    {
        return await JsonHelper.ReadJsonAsync<Repository[]>(path) ?? Array.Empty<Repository>();
    }

    public Task WriteRepositoriesAsync(IEnumerable<Repository> repositories, string path)
    {
        return JsonHelper.AppendRangeJsonAsync(repositories, path);
    }
}