using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Application.LanguageStatistics;

public class LanguageStatisticsFactory : ISingletonDependency
{
    private readonly ILogger<LanguageStatisticsFactory> _logger;
    private readonly IServiceProvider _serviceProvider;

    public LanguageStatisticsFactory(IServiceProvider serviceProvider, ILogger<LanguageStatisticsFactory> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public ILanguageStatistics GetLanguageStatistics(Language language)
    {
        var languageStatistics = _serviceProvider.GetRequiredService<IEnumerable<ILanguageStatistics>>()
            .SingleOrDefault(t =>
                t.GetType().GetCustomAttribute<LanguageAttribute>()?.Languages.Contains(language) ?? false);

        if (languageStatistics == null)
        {
            _logger.LogWarning($"No language statistics found for language {language}");
            return _serviceProvider.GetRequiredService<NullLanguageStatistics>();
        }

        return languageStatistics;
    }

    public Language[] GetSupportedLanguages()
    {
        return _serviceProvider.GetRequiredService<IEnumerable<ILanguageStatistics>>()
            .SelectMany(t => t.GetType().GetCustomAttribute<LanguageAttribute>()?.Languages ?? Array.Empty<Language>())
            .Distinct()
            .ToArray();
    }
}