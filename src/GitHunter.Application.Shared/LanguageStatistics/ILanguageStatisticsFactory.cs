using Octokit;

namespace GitHunter.Application.LanguageStatistics;

public interface ILanguageStatisticsFactory
{
    ILanguageStatistics GetLanguageStatistics(Language language);
    
    Language[] GetSupportedLanguages();
}