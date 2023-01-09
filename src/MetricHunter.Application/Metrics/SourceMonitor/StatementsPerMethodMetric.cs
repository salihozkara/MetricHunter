namespace MetricHunter.Application.Metrics.SourceMonitor;

public class StatementsPerMethodMetric : ISourceMonitorMetric
{
    public StatementsPerMethodMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[] { "Statements per Method", "Average Statements per Method" };

    public string Name => "Statements Per Method";
    public string Value { get; }
}