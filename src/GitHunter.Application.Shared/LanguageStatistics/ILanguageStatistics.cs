using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Application.LanguageStatistics;

public interface ILanguageStatistics : ISingletonDependency
{
    Task GetStatisticsAsync(Repository repository, CancellationToken token = default);
}