using System.Reflection;
using MetricHunter.Core.Languages;

namespace MetricHunter.Application.Git;

public static class GitConsts
{
    public const string RepositoryInfoFileExtension = ".metrichunterinfo";
    
    public static readonly Dictionary<string, Language> LanguagesMap = Enum.GetValues<Language>().ToDictionary(x => x.GetGitHubLanguage(), x=>x);
}