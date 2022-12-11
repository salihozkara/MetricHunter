using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Application.LanguageStatistics;

public interface IMetricCalculator : ISingletonDependency
{
    Task CalculateMetricsAsync(Repository repository, CancellationToken token = default);
}