namespace MetricHunter.Application.Metrics;

public class Metric : IMetric
{
    public Metric(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; }
    public string Value { get; }
}