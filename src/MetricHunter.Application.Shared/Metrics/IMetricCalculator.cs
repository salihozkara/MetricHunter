using MetricHunter.Application.Results;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Application.Metrics;

public interface IMetricCalculator : ISingletonDependency
{
    Task<IResult> CalculateMetricsAsync(Repository repository, string baseRepositoriesDirectoryPath = "",
        string baseReportsDirectoryPath = "", CancellationToken token = default);

    Task<IResult[]> CalculateMetricsByLocalResultsAsync(List<Repository> repositories, string baseDirectoryPath = "",
        CancellationToken token = default);
}