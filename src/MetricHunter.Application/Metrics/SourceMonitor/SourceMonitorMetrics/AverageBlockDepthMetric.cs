namespace MetricHunter.Application.Metrics.SourceMonitor.SourceMonitorMetrics;

public class AverageBlockDepthMetric : ISourceMonitorMetric
{
    public AverageBlockDepthMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[] { "Average Block Depth" };

    public string Name => "Average Block Depth";
    public string Value { get; }
}