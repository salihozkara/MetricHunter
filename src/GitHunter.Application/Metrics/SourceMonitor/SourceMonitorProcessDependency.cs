using GitHunter.Application.Resources;
using GitHunter.Core.DependencyProcesses;

namespace GitHunter.Application.Metrics.SourceMonitor;

public class SourceMonitorProcessDependency : IProcessDependency
{
    public bool Check()
    {
        return File.Exists(Resource.SourceMonitor.SourceMonitorExe.Path);
    }

    public string ProcessName => "SourceMonitor";

    public string ErrorMessage =>
        $"SourceMonitor.exe not found. Please download it from {DownloadUrl} and place it in the same folder as GitHunter Resources.";

    public string ErrorTitle => "SourceMonitor.exe not found";
    public string DownloadUrl => "https://www.campwoodsw.com/sourcemonitor.html";
}