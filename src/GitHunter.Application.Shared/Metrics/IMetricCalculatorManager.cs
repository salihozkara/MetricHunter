using Octokit;

namespace GitHunter.Application.Metrics;

public interface IMetricCalculatorManager
{
    IMetricCalculator FindMetricCalculator(Language language);

    Language[] GetSupportedLanguages();
}