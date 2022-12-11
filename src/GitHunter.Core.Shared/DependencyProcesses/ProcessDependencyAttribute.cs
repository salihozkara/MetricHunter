namespace GitHunter.Core.DependencyProcesses;


public abstract class ProcessDependencyAttribute : Attribute
{
    public Type? DependencyProcess { get; protected init; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ProcessDependencyAttribute<T> : ProcessDependencyAttribute  where T : IProcessDependency
{
    public ProcessDependencyAttribute()
    {
        DependencyProcess = typeof(T);
    }
}