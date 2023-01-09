namespace MetricHunter.Application.Metrics.SourceMonitor;

public class MaximumBlockDepthMetric : ISourceMonitorMetric
{
    public MaximumBlockDepthMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[] { "Maximum Block Depth" };

    public string Name => "Maximum Block Depth";
    public string Value { get; }
}