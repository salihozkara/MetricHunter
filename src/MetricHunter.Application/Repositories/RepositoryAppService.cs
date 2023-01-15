using System.Text;
using MetricHunter.Application.Resources;
using MetricHunter.Core.Jsons;
using MetricHunter.Core.Paths;
using Newtonsoft.Json;
using Octokit;
using Volo.Abp.DependencyInjection;
using FileMode = System.IO.FileMode;

namespace MetricHunter.Application.Repositories;

public class RepositoryAppService : IRepositoryAppService, ISingletonDependency
{
    public async Task<Repository[]> ReadRepositories(string path)
    {
        return await JsonHelper.ReadJsonAsync<Repository[]>(path) ?? Array.Empty<Repository>();
    }

    public Task WriteRepositories(IEnumerable<Repository> repositories, string path)
    {
        return JsonHelper.AppendRangeJsonAsync(repositories, path);
    }

    public async Task<List<Repository>> GetRepositoriesFromPaths(FilePath[] infoFiles)
    {
        var enumerable = infoFiles.Select(async x => await JsonHelper.ReadJsonAsync<Repository>(x.FullPath));
        return (await Task.WhenAll(enumerable)).Where(x=>x != null).Select(x=>x!).ToList();
    }
}