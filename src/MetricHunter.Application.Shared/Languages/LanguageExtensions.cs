using Octokit;

namespace MetricHunter.Application.Languages;

public static class LanguageExtensions
{
    private static readonly Dictionary<string, Language> OctokitLanguagesMap = Enum.GetValues<Language>().ToDictionary(x => x.ToString(), x => x);
    
    
    public static Language? ConvertToOctokitLanguage(this Core.Languages.Language? language)
    {
        if(OctokitLanguagesMap.TryGetValue(language?.ToString() ?? string.Empty, out var result))
        {
            return result;
        }
        
        return null;
    }
}