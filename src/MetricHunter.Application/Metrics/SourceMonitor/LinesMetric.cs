namespace MetricHunter.Application.Metrics.SourceMonitor;

public class LinesMetric : ISourceMonitorMetric
{
    public LinesMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[] { "Lines" };

    public string Name => "Lines";
    public string Value { get; }
}