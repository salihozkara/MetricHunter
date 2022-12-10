using GitHunter.Application;
using GitHunter.Core.Modules;
using Volo.Abp.Modularity;

namespace GitHunter.Desktop;

[DependsOn(typeof(ApplicationSharedModule))]
public class DesktopSharedModule : GitHunterModule
{
}