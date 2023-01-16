using MetricHunter.Core.Paths;
using MetricHunter.Core.Strings;

namespace MetricHunter.Desktop.DesktopLogs;

public class DesktopLogsConsts
{
    public static readonly FilePath LogFilePath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\MetricHunter.log".ToFilePath();
}