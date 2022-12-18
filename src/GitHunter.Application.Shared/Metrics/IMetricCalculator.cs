using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Application.Metrics;

public interface IMetricCalculator : ISingletonDependency
{
    Task<List<IMetric>> CalculateMetricsAsync(Repository repository, CancellationToken token = default);
}