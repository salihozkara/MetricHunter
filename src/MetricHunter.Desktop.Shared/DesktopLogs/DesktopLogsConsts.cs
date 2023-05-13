using AdvancedPath;
using MetricHunter.Core.Paths;

namespace MetricHunter.Desktop.DesktopLogs;

public class DesktopLogsConsts
{
    public static readonly FilePathString LogFilePath = PathHelper.TempPath + "Logs\\MetricHunter.log".ToFilePathString();
}