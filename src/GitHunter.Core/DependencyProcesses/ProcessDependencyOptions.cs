namespace GitHunter.Core.DependencyProcesses;

public class ProcessDependencyOptions
{
    public Type? StartupModule { get; set; }

    public Action? ErrorAction { get; set; }
}