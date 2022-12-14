using System.Diagnostics;
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
            o.ErrorAction = (pd) =>
            {
                var text = pd?.ErrorMessage + Environment.NewLine + "Do you want to download it now?";
                if(MessageBox.Show(text, pd?.ErrorTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    if (pd?.DownloadUrl != null) Process.Start("explorer", pd.DownloadUrl);
                }
                Environment.Exit(0);
            };
        });

        base.ConfigureServices(context);
    }

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        Configure<ProcessDependencyOptions>(o => o.StartupModule = typeof(DesktopWindowsFormModule));

        base.PreConfigureServices(context);
    }
}