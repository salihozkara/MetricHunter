using AdvancedPath;

namespace MetricHunter.Desktop.DesktopLogs;

public class DesktopLogsConsts
{
    public static readonly FilePathString LogFilePath =
        AppDomain.CurrentDomain.BaseDirectory + "Logs\\MetricHunter.log".ToFilePathString();
}