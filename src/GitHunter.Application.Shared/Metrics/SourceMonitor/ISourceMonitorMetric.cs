namespace GitHunter.Application.Metrics.SourceMonitor;

public interface ISourceMonitorMetric : IMetric
{
    static IReadOnlyList<string> MatchedMetricNames { get; }
}