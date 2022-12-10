using GitHunter.Core;
using GitHunter.Core.Modules;
using Volo.Abp.Modularity;

namespace GitHunter.Application;

[DependsOn(
    typeof(ApplicationSharedModule),
    typeof(CoreModule)
)]
public class ApplicationModule : GitHunterModule
{
}