using System.Dynamic;
using System.Globalization;
using CsvHelper;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Application.Csv;

public class CsvHelper : ICsvHelper, ISingletonDependency
{
    public string MetricsToCsv(List<Dictionary<string, string>> metrics)
    {
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(ToExpandoObject(metrics));

        return writer.ToString();
    }

    private static dynamic ToExpandoObject(Dictionary<string, string> dictionary)
    {
        var expando = new ExpandoObject();
        var expandoDict = expando as IDictionary<string, object>;
        foreach (var kvp in dictionary) expandoDict.Add(kvp.Key, kvp.Value);
        return expando;
    }

    private static IEnumerable<dynamic> ToExpandoObject(IEnumerable<Dictionary<string, string>> dictionaries)
    {
        return dictionaries.Select(ToExpandoObject);
    }
}