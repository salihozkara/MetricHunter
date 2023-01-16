﻿namespace MetricHunter.Application.Metrics.SourceMonitor.SourceMonitorMetrics;

public class StatementsMetric : ISourceMonitorMetric
{
    public StatementsMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[] { "Statements" };

    public string Name => "Statements";
    public string Value { get; }
}