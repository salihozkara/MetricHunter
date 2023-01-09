namespace MetricHunter.Core.DependencyProcesses;

public class ProcessDependencyOptions
{
    public Type? StartupModule { get; set; }

    public Action<IProcessDependency?>? ErrorAction { get; set; }
}