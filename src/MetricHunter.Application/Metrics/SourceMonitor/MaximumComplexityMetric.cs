namespace MetricHunter.Application.Metrics.SourceMonitor;

public class MaximumComplexityMetric : ISourceMonitorMetric
{
    public MaximumComplexityMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[] { "Maximum Complexity" };

    public string Name => "Maximum Complexity";
    public string Value { get; }
}