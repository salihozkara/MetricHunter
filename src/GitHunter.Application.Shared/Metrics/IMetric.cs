namespace GitHunter.Application.Metrics;

public interface IMetric
{
    string Name { get; }
    string Value { get; }
}