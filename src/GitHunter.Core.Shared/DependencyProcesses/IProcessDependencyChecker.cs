using System.Reflection;

namespace GitHunter.Core.DependencyProcesses;

public interface IProcessDependencyChecker
{
    bool CheckDependency(Assembly assembly, out IProcessDependency? dependency);
}