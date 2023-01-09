using System.Reflection;

namespace MetricHunter.Core.DependencyProcesses;

public interface IProcessDependencyChecker
{
    bool CheckDependency(Assembly assembly, out IProcessDependency? dependency);
}