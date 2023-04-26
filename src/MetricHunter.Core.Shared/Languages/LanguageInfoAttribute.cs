namespace MetricHunter.Core.Languages;

[AttributeUsage(AttributeTargets.Field)]
public class LanguageInfoAttribute : Attribute
{
    private string? _gitHubLanguage;

    public LanguageInfoAttribute(string normalizedName, string parameterName)
    {
        NormalizedName = normalizedName;
        ParameterName = parameterName;
    }

    public string NormalizedName { get; }
    public string ParameterName { get; }

    public string GitHubLanguage
    {
        get => string.IsNullOrWhiteSpace(_gitHubLanguage) ? NormalizedName : _gitHubLanguage;
        set => _gitHubLanguage = value;
    }
}