using GitHunter.Application.Shared;
using Volo.Abp.Modularity;

namespace GitHunter.Application;

[DependsOn(typeof(ApplicationSharedModule))]
public class ApplicationModule : AbpModule
{
}