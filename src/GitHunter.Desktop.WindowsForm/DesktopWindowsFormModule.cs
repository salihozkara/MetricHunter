using GitHunter.Application;
using GitHunter.Core.DependencyProcesses;
using GitHunter.Core.Desktop;
using GitHunter.Core.Modules;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using ApplicationInitializationContext = Volo.Abp.ApplicationInitializationContext;

namespace GitHunter.Desktop;

[DependsOn(typeof(DesktopSharedModule), 
    typeof(ApplicationModule))]
public class DesktopWindowsFormModule : GitHunterModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<ProcessDependencyOptions>(o=>
        {
            o.StartupModule = typeof(DesktopWindowsFormModule);
            o.ErrorAction = () =>
            {
                MessageBox.Show("Error");
            };
        });
        
        base.ConfigureServices(context);
    }

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        Configure<ProcessDependencyOptions>(o=>o.StartupModule = typeof(DesktopWindowsFormModule));

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