namespace MetricHunter.Application.Metrics;

public interface IMetric
{
    string Name { get; }
    string Value { get; }
}