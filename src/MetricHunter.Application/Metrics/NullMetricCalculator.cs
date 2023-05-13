using MetricHunter.Application.Repositories;
using MetricHunter.Application.Results;
using Microsoft.Extensions.Logging;

namespace MetricHunter.Application.Metrics;

public class NullMetricCalculator : IMetricCalculator
{
    private readonly ILogger<NullMetricCalculator> _logger;

    public NullMetricCalculator(ILogger<NullMetricCalculator> logger)
    {
        _logger = logger;
    }
    
    public Task<IResult> CalculateMetricsAsync(RepositoryWithBranchNameDto repositoryWithBranchNameDto,
        string baseRepositoriesDirectoryPath = "", string baseReportsDirectoryPath = "",
        CancellationToken token = default)
    {
        _logger.LogInformation("No language statistics available for {Repository}",
            repositoryWithBranchNameDto.Repository.Name);
        return Task.FromResult<IResult>(new NullResult());
    }
}