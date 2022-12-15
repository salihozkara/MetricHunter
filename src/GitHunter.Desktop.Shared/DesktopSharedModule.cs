using GitHunter.Application;
using GitHunter.Core.Modules;
using GitHunter.Desktop.Core;
using Volo.Abp.Modularity;

namespace GitHunter.Desktop;

[DependsOn(
    typeof(ApplicationSharedModule),
    typeof(DesktopCoreSharedModule)
    )]
public class DesktopSharedModule : GitHunterModule
{
}