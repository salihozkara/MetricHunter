using MetricHunter.Core.Processes;
using MetricHunter.Desktop.DesktopLogs;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;

namespace MetricHunter.Desktop;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/logs.txt"))
            .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}")
            .WriteTo.Desktop()
            .CreateLogger();

        using var application = AbpApplicationFactory.Create<DesktopWindowsFormModule>(
            options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            });

        application.Initialize();

        var processManager = application.ServiceProvider
            .GetRequiredService<IProcessManager>();
        processManager.KillAllProcesses();

        application.Shutdown();
    }
}