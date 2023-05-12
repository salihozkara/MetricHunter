using MetricHunter.Core.Jsons;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Application.Repositories;

public class RepositoryAppService : IRepositoryAppService, ISingletonDependency
{
    public async Task<RepositoryWithBranchNameDto[]> ReadRepositoriesAsync(string path, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await JsonHelper.ReadJsonAsync<RepositoryWithBranchNameDto[]>(path, cancellationToken) ?? Array.Empty<RepositoryWithBranchNameDto>();
    }

    public Task WriteRepositoriesAsync(IEnumerable<RepositoryWithBranchNameDto> repositories, string path,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return JsonHelper.AppendRangeJsonAsync(repositories, path, cancellationToken);
    }
}