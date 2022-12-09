using GitHunter.Application.Shared;
using Volo.Abp.Modularity;

namespace GitHunter.Desktop.Shared;

[DependsOn(typeof(ApplicationSharedModule))]
public class DesktopSharedModule : AbpModule
{
}