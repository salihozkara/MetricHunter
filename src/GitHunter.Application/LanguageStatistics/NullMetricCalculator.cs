using Microsoft.Extensions.Logging;
using Octokit;

namespace GitHunter.Application.LanguageStatistics;

public class NullMetricCalculator : IMetricCalculator
{
    private readonly ILogger<NullMetricCalculator> _logger;

    public NullMetricCalculator(ILogger<NullMetricCalculator> logger)
    {
        _logger = logger;
    }

    public Task CalculateMetricsAsync(Repository repository, CancellationToken token = default)
    {
        _logger.LogInformation("No language statistics available for {Repository}", repository.Name);
        return Task.CompletedTask;
    }
}