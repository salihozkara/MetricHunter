namespace GitHunter.Application.Csv;

public interface ICsvHelper
{
    string MetricsToCsv(List<Dictionary<string, string>> metrics);
}