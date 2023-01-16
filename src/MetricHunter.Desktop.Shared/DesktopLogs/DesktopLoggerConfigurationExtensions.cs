using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace MetricHunter.Desktop.DesktopLogs;

public static class DesktopLoggerConfigurationExtensions
{
    private const string DefaultOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static LoggerConfiguration Desktop(
        this LoggerSinkConfiguration sinkConfiguration,
        LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose,
        string outputTemplate = DefaultOutputTemplate)
    {
        if (sinkConfiguration == null)
            throw new ArgumentNullException(nameof(sinkConfiguration));
        if (outputTemplate == null)
            throw new ArgumentNullException(nameof(outputTemplate));

        return sinkConfiguration.Sink(new DesktopSink(outputTemplate), restrictedToMinimumLevel);
    }
}