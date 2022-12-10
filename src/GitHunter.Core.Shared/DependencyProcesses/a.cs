namespace GitHunter.Core.DependencyProcesses;

public abstract class ProcessDependencyAttribute : Attribute
{
    public Type DependencyProcess { get; protected set; }
}