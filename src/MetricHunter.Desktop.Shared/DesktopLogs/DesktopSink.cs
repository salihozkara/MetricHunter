using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;

namespace MetricHunter.Desktop.DesktopLogs;

public class DesktopSink : ILogEventSink
{
    private readonly ITextFormatter _textFormatter;

    public DesktopSink(string outputTemplate)
    {
        _textFormatter = new MessageTemplateTextFormatter(outputTemplate);
    }

    public static Action<string, LogEvent> LogAction { get; set; }

    public void Emit(LogEvent logEvent)
    {
        if (logEvent == null)
            throw new ArgumentNullException(nameof(logEvent));

        var output = new StringWriter();
        _textFormatter.Format(logEvent, output);
        LogAction?.Invoke(output.ToString(), logEvent);
    }
}