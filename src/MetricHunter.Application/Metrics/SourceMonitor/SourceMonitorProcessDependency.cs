using MetricHunter.Application.Resources;
using MetricHunter.Core.DependencyProcesses;

namespace MetricHunter.Application.Metrics.SourceMonitor;

public class SourceMonitorProcessDependency : IProcessDependency
{
    public bool Check()
    {
        return Resource.SourceMonitor.SourceMonitorExe.Exists;
    }

    public string ProcessName => "SourceMonitor";

    public string ErrorMessage =>
        $"SourceMonitor.exe not found. Please download it from {DownloadUrl} and place it in the same folder as MetricHunter Resources.";

    public string ErrorTitle => "SourceMonitor.exe not found";
    public string DownloadUrl => "https://www.campwoodsw.com/sourcemonitor.html";
}