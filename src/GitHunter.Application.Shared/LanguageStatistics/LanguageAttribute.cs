using Octokit;

namespace GitHunter.Application.LanguageStatistics;

public class LanguageAttribute : Attribute
{
    public LanguageAttribute(params Language[] languages)
    {
        Languages = languages;
    }

    public Language[] Languages { get; }
}