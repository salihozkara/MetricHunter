using Microsoft.Extensions.Logging;
using Octokit;

namespace GitHunter.Application.LanguageStatistics;

public class NullLanguageStatistics : ILanguageStatistics
{
    private readonly ILogger<NullLanguageStatistics> _logger;

    public NullLanguageStatistics(ILogger<NullLanguageStatistics> logger)
    {
        _logger = logger;
    }

    public Task GetStatisticsAsync(Repository repository, CancellationToken token = default)
    {
        _logger.LogInformation("No language statistics available for {Repository}", repository.Name);
        return Task.CompletedTask;
    }
}