using MetricHunter.Core.Paths;
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

        AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
        {
            Log.Fatal(eventArgs.ExceptionObject as Exception, "Unhandled exception");
            MessageBox.Show(eventArgs.ExceptionObject.ToString(), "Unhandled Exception");
        };


        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(DesktopLogsConsts.LogFilePath, rollingInterval: RollingInterval.Month)
            .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}")
            .WriteTo.Desktop()
            .CreateLogger();

        using var application = AbpApplicationFactory.Create<DesktopWindowsFormModule>(
            options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            });

        try
        {
            application.Initialize();
        }
        finally
        {
            var processManager = application.ServiceProvider
                .GetRequiredService<IProcessManager>();
            processManager.KillAllProcesses();
        

            // Delete temp files
            PathHelper.DeleteTempFiles();
        

            application.Shutdown();
            
            Log.CloseAndFlush();
        }
    }
}