using System.Reflection;

namespace MetricHunter.Core.Languages;

public enum Language
{
    C,
    [LanguageInfo("C++", "cpp")] 
    CPlusPlus,
    [LanguageInfo("C#", "csharp")] 
    CSharp,
    [LanguageInfo("Pascal", "delphi")]
    Delphi,
    Html,
    Java,
    [LanguageInfo("VB.NET", "vbnet", GitHubLanguage = "Visual Basic .NET")] 
    VBNET
}

public static class LanguageExtensions
{
    public static string GetGitHubLanguage(this Language language)
    {
        var info = language.GetType().GetField(language.ToString())?.GetCustomAttribute<LanguageInfoAttribute>();
        return info?.GitHubLanguage ?? language.ToString();
    }
}