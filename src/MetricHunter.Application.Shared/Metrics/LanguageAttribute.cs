using Octokit;

namespace MetricHunter.Application.Metrics;

public class LanguageAttribute : Attribute
{
    public LanguageAttribute(params Language[] languages)
    {
        Languages = languages;
    }

    public Language[] Languages { get; }
}