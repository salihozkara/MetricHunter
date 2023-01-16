using MetricHunter.Core.Jsons;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Application.Repositories;

public class RepositoryAppService : IRepositoryAppService, ISingletonDependency
{
    public async Task<Repository[]> ReadRepositoriesAsync(string path, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await JsonHelper.ReadJsonAsync<Repository[]>(path, cancellationToken) ?? Array.Empty<Repository>();
    }

    public Task WriteRepositoriesAsync(IEnumerable<Repository> repositories, string path,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return JsonHelper.AppendRangeJsonAsync(repositories, path, cancellationToken);
    }
}