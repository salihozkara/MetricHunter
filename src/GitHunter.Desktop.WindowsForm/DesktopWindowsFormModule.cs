using GitHunter.Desktop.Shared;
using Volo.Abp.Modularity;

namespace GitHunter.Desktop.WindowsForm;

[DependsOn(typeof(DesktopSharedModule))]
public class DesktopWindowsFormModule : AbpModule
{
}