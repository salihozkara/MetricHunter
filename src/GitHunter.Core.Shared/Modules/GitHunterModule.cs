using System.Reflection;
using Volo.Abp.Modularity;

namespace GitHunter.Core.Modules;

public class GitHunterModule : AbpModule
{
    public static bool IsGitHunterModule(Type type)
    {
        var typeInfo = type.GetTypeInfo();

        return
            typeInfo.IsClass &&
            !typeInfo.IsAbstract &&
            !typeInfo.IsGenericType &&
            typeof(GitHunterModule).GetTypeInfo().IsAssignableFrom(type);
    }
}