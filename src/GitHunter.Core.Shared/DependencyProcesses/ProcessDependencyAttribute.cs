namespace GitHunter.Core.DependencyProcesses;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ProcessDependencyAttribute<T> : ProcessDependencyAttribute  where T : IProcessDependency
{
    public ProcessDependencyAttribute()
    {
        DependencyProcess = typeof(T);
    }
}