using Octokit;

namespace GitHunter.Application.LanguageStatistics;

public interface IMetricCalculatorManager
{
    IMetricCalculator FindMetricCalculator(Language language);
    
    Language[] GetSupportedLanguages();
}