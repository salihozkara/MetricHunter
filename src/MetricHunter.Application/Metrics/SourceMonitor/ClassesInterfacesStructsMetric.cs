namespace MetricHunter.Application.Metrics.SourceMonitor;

public class ClassesInterfacesStructsMetric : ISourceMonitorMetric
{
    public ClassesInterfacesStructsMetric(string value)
    {
        Value = value;
    }

    public static string[] MatchedMetricNames => new[]
        { "Classes, Interfaces, Structs", "Classes and Interfaces", "Classes Defined" };

    public string Name => "Classes, Interfaces, Structs";
    public string Value { get; }
}