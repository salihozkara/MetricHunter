namespace MetricHunter.Application.Metrics.SourceMonitor;

public class AverageComplexityMetric : ISourceMonitorMetric
{
    public AverageComplexityMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[] { "Average Complexity" };

    public string Name => "Average Complexity";
    public string Value { get; }
}