using AdvancedPath;
using MetricHunter.Core.Paths;

namespace MetricHunter.Desktop.DesktopLogs;

public class DesktopLogsConsts
{
    public static readonly FilePathString LogFilePath = PathHelper.AppDataPath + "Logs\\MetricHunter.log".ToFilePathString();
}