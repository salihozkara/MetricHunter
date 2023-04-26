using System.Dynamic;
using System.Globalization;
using CsvHelper;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Application.Csv;

public class CsvHelper : ICsvHelper, ISingletonDependency
{
    public string MetricsToCsv(List<Dictionary<string, string>> metrics)
    {
        var records = ConvertRecords(metrics);
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(records);

        return writer.ToString();
    }

    private static dynamic ToExpandoObject(Dictionary<string, string> dictionary)
    {
        var expando = new ExpandoObject();
        var expandoDict = expando as IDictionary<string, object>;
        foreach (var kvp in dictionary) expandoDict.Add(kvp.Key, kvp.Value);
        return expando;
    }
    
    private static IEnumerable<object> ConvertRecords(IReadOnlyCollection<Dictionary<string, string>> metrics)
    {
        var keys = metrics.SelectMany(x => x.Keys).Distinct().ToList();
        var result = metrics.Select(metric => keys.ToDictionary(key => key, key => metric.TryGetValue(key, out var value) ? value : "N/A")).ToList();
        return result.Select(ToExpandoObject);
    }
}