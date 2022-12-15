using GitHunter.Core;
using Volo.Abp.Modularity;

namespace GitHunter.Desktop.Core;

[DependsOn(typeof(CoreSharedModule))]
public class DesktopCoreSharedModule : AbpModule
{
}