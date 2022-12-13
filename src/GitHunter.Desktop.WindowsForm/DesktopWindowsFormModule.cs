using GitHunter.Application;
using GitHunter.Core.DependencyProcesses;
using GitHunter.Core.Modules;
using Volo.Abp.Modularity;

namespace GitHunter.Desktop;

[DependsOn(typeof(DesktopSharedModule),
    typeof(ApplicationModule))]
public class DesktopWindowsFormModule : GitHunterModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<ProcessDependencyOptions>(o =>
        {
            o.StartupModule = typeof(DesktopWindowsFormModule);
            o.ErrorAction = () => { MessageBox.Show("Error"); };
        });

        base.ConfigureServices(context);
    }

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        Configure<ProcessDependencyOptions>(o => o.StartupModule = typeof(DesktopWindowsFormModule));

        base.PreConfigureServices(context);
    }
}