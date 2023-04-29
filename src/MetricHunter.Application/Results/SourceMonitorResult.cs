using System.Globalization;
using System.Text.RegularExpressions;
using MetricHunter.Application.Metrics;
using Octokit;

namespace MetricHunter.Application.Results;

public class SourceMonitorResult : IResult
{
    public SourceMonitorResult(Repository repository, List<IMetric> metrics)
    {
        Metrics = metrics;
        Id = repository.Id;
        Owner = repository.Owner.Login;
        Name = repository.Name;
        Description = repository.Description;
        Language = repository.Language;
        Stars = repository.StargazersCount;
        Forks = repository.ForksCount;
        Size = repository.Size;
        CreatedAt = repository.CreatedAt;
        UpdatedAt = repository.UpdatedAt;
        PushedAt = repository.PushedAt;
        Topics = repository.Topics;
        IsTemplate = repository.IsTemplate;
        DefaultBranch = repository.DefaultBranch;
        OpenIssuesCount = repository.OpenIssuesCount;
        HasIssues = repository.HasIssues;
        HasWiki = repository.HasWiki;
        HasDownloads = repository.HasDownloads;
        HasPages = repository.HasPages;
        Archived = repository.Archived;
        SubscribersCount = repository.SubscribersCount;
    }

    public long Id { get; }
    public string Owner { get; }
    public string Name { get; }
    public string Description { get; }
    public string Language { get; }
    public int Stars { get; }
    public int Forks { get; }
    public long Size { get; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset UpdatedAt { get; }
    public DateTimeOffset? PushedAt { get; }
    public IReadOnlyList<string> Topics { get; }
    public bool IsTemplate { get; }
    public string DefaultBranch { get; }
    public int OpenIssuesCount { get; }
    public bool HasIssues { get; }
    public bool HasWiki { get; }
    public bool HasDownloads { get; }
    public bool HasPages { get; }
    public bool Archived { get; }
    public int SubscribersCount { get; }

    public List<IMetric> Metrics { get; }


    public Dictionary<string, string> ToDictionary()
    {
        var dictionary = new Dictionary<string, string>
        {
            { Normalize(nameof(Id)), Id.ToString() },
            { Normalize(nameof(Owner)), Owner },
            { Normalize(nameof(Name)), Name },
            { Normalize(nameof(Description)), Description },
            { Normalize(nameof(Language)), Language },
            { Normalize(nameof(Stars)), Stars.ToString() },
            { Normalize(nameof(Forks)), Forks.ToString() },
            { Normalize(nameof(Size)), Size.ToString() },
            { Normalize(nameof(CreatedAt)), CreatedAt.ToUnixTimeSeconds().ToString() },
            { Normalize(nameof(UpdatedAt)), UpdatedAt.ToUnixTimeSeconds().ToString() },
            { Normalize(nameof(PushedAt)), (PushedAt ?? CreatedAt).ToUnixTimeSeconds().ToString() },
            { Normalize(nameof(Topics)), Topics.JoinAsString("|") },
            { Normalize(nameof(IsTemplate)), IsTemplate.ToString() },
            { Normalize(nameof(DefaultBranch)), DefaultBranch },
            { Normalize(nameof(OpenIssuesCount)), OpenIssuesCount.ToString() },
            { Normalize(nameof(HasIssues)), HasIssues.ToString() },
            { Normalize(nameof(HasWiki)), HasWiki.ToString() },
            { Normalize(nameof(HasDownloads)), HasDownloads.ToString() },
            { Normalize(nameof(HasPages)), HasPages.ToString() },
            { Normalize(nameof(Archived)), Archived.ToString() },
            { Normalize(nameof(SubscribersCount)), SubscribersCount.ToString() }
        };

        foreach (var metric in Metrics) dictionary[Normalize(metric.Name)] = metric.Value;

        return dictionary;
    }

    public List<Dictionary<string, string>> ToDictionaryListByTopics()
    {
        var dict = ToDictionary();
        if (Topics.Count == 0)
            return new List<Dictionary<string, string>> { dict };
        var list = new List<Dictionary<string, string>>();
        foreach (var topic in Topics)
        {
            var newDict = new Dictionary<string, string>();
            foreach (var (key, value) in dict.Where(d => d.Key != Normalize(nameof(Topics)))) newDict.Add(key, value);

            newDict[Normalize(nameof(Topics))] = topic;
            list.Add(newDict);
        }

        return list;
    }

    public bool IsEmpty()
    {
        return Metrics.All(
            m => m.Value is "0" or "0.0" or "0,0" or "0.00" or "0,00" or "0.000" or "0,000" or "" or null);
    }

    private string Normalize(string value)
    {
        var cultureInfo = new CultureInfo("en-US", false);
        return Regex
            .Replace(value.Replace(" ", "_"), "(?<!^)([A-Z])", "_$1", RegexOptions.Compiled, TimeSpan.FromSeconds(1))
            .ToLower(cultureInfo).Replace("__", "_");
    }
}