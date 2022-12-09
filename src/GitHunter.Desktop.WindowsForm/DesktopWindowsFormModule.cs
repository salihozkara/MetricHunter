using GitHunter.Application;
using GitHunter.Desktop.Shared;
using Volo.Abp.Modularity;

namespace GitHunter.Desktop.WindowsForm;

[DependsOn(typeof(DesktopSharedModule), 
    typeof(ApplicationModule))]
public class DesktopWindowsFormModule : AbpModule
{
}