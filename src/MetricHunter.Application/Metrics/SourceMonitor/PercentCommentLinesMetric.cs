namespace MetricHunter.Application.Metrics.SourceMonitor;

public class PercentCommentLinesMetric : ISourceMonitorMetric
{
    public PercentCommentLinesMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[] { "Percent Comment Lines", "Percent Lines With Comments" };

    public string Name => "Percent Comment Lines";
    public string Value { get; }
}