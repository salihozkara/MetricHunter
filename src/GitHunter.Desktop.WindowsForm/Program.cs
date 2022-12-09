using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;

namespace GitHunter.Desktop.WindowsForm;

public class Program
{
    [STAThread]
    public static async Task Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "Logs/logs.txt"))
            .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}")
            .CreateLogger();


        using var application = await AbpApplicationFactory.CreateAsync<DesktopWindowsFormModule>(
            options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            });
        await application.InitializeAsync();
        
        var mainForm = application.ServiceProvider
            .GetRequiredService<MainForm>();
        
        System.Windows.Forms.Application.Run(mainForm);
        
        await application.ShutdownAsync();
    }
}