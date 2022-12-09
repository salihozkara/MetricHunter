using GitHunter.Application.Shared;
using GitHunter.Core;
using Volo.Abp.Modularity;

namespace GitHunter.Application;

[DependsOn(
    typeof(ApplicationSharedModule),
    typeof(CoreModule)
)]
public class ApplicationModule : AbpModule
{
}