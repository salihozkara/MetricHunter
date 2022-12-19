namespace GitHunter.Application.Metrics.SourceMonitor;

public class MethodsPerClassMetric : ISourceMonitorMetric
{
    public MethodsPerClassMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[] { "Methods per Class", "Methods Implemented per Class" };

    public string Name => "Methods Per Class";
    public string Value { get; }
}