using GitHunter.Application.Resources;
using GitHunter.Core.DependencyProcesses;

namespace GitHunter.Application.Metrics.SourceMonitor;

public class SourceMonitorProcessDependency : IProcessDependency
{
    public bool Check()
    {
        return File.Exists(Resource.SourceMonitor.SourceMonitorExe.Path);
    }
}