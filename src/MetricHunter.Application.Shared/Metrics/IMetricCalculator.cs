using MetricHunter.Application.Repositories;
using MetricHunter.Application.Results;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Application.Metrics;

public interface IMetricCalculator : ISingletonDependency
{
    Task<IResult> CalculateMetricsAsync(RepositoryWithBranchNameDto repositoryWithBranchNameDto, string baseRepositoriesDirectoryPath = "",
        string baseReportsDirectoryPath = "", CancellationToken token = default);
}