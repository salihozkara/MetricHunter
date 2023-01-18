namespace MetricHunter.Application.Metrics.SourceMonitor.SourceMonitorMetrics;

public class PercentDocumentationLinesMetric : ISourceMonitorMetric
{
    public PercentDocumentationLinesMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[] { "Percent Documentation Lines" };

    public string Name => "Percent Documentation Lines";
    public string Value { get; }
}