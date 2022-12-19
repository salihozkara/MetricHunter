using GitHunter.Application.Results;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Application.Metrics;

public interface IMetricCalculator : ISingletonDependency
{
    Task<IResult>? CalculateMetricsAsync(Repository repository, CancellationToken token = default);
}