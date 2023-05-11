using System.Reflection;

namespace MetricHunter.Core.Languages;

public enum Language
{
    C,
    [LanguageInfo("C++", "cpp")] 
    CPlusPlus,
    [LanguageInfo("C#", "csharp")] 
    CSharp,
    [LanguageInfo("Delphi", "pascal", GitHubLanguage = "Pascal")]
    Delphi,
    Html,
    Java,
    [LanguageInfo("VB.NET", "vbnet", GitHubLanguage = "Visual Basic .NET")] 
    VBNET
}

public static class LanguageExtensions
{
    public static string GetParameterName(this Language language)
    {
        var info = language.GetType().GetField(language.ToString())?.GetCustomAttribute<LanguageInfoAttribute>();
        return info?.ParameterName ?? language.ToString();
    }
    
    public static string GetNormalizedLanguage(this Language language)
    {
        var info = language.GetType().GetField(language.ToString())?.GetCustomAttribute<LanguageInfoAttribute>();
        return info?.NormalizedName ?? language.ToString();
    }
    
    public static string GetGitHubLanguage(this Language language)
    {
        var info = language.GetType().GetField(language.ToString())?.GetCustomAttribute<LanguageInfoAttribute>();
        return info?.GitHubLanguage ?? language.ToString();
    }
}