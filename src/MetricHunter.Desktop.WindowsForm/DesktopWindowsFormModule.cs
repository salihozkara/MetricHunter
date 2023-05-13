using System.Diagnostics;
using System.Reflection;
using AdvancedPath;
using MetricHunter.Application;
using MetricHunter.Application.Resources;
using MetricHunter.Core.DependencyProcesses;
using MetricHunter.Core.Modules;
using MetricHunter.Desktop.Core;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace MetricHunter.Desktop;

[DependsOn(typeof(DesktopSharedModule),
    typeof(ApplicationModule))]
public class DesktopWindowsFormModule : MetricHunterModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<ProcessDependencyOptions>(o =>
        {
            o.StartupModule = typeof(DesktopWindowsFormModule);
            o.ErrorAction = pd =>
            {
                var text = pd?.ErrorMessage + Environment.NewLine + "Do you want to download it now?";
                if (MessageBox.Show(text, pd?.ErrorTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) ==
                    DialogResult.Yes)
                    if (pd?.DownloadUrl != null)
                        Process.Start("explorer", pd.DownloadUrl);
                Environment.Exit(0);
            };
        });

        base.ConfigureServices(context);
    }

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        Configure<ProcessDependencyOptions>(o => o.StartupModule = typeof(DesktopWindowsFormModule));

        Resource.GetOrCreateFile = path =>
        {
            FilePathString filePathString = path;
            if (File.Exists(path)) return path.ToFilePathString();
            var name = path.Replace(Resource.DynamicResFolder, string.Empty).Replace("\\", ".").Replace("/", ".");
            var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var resourceName = resourceNames.FirstOrDefault(x => x.EndsWith(name));
            if (resourceName == null)
                throw new Exception($"Resource {path} not found");
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new Exception($"Resource {path} not found");
            filePathString.ParentDirectory.CreateIfNotExists();
            using var fileStream = File.Create(path);
            stream.CopyTo(fileStream);
            return filePathString;
        };

        base.PreConfigureServices(context);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.ServiceProvider
            .GetRequiredService<IApplicationController>();

        app.StartApplication();

        base.OnApplicationInitialization(context);
    }
}