using GitHunter.Core.Shared;
using Volo.Abp.Modularity;

namespace GitHunter.Core;

[DependsOn(typeof(CoreSharedModule))]
public class CoreModule : AbpModule
{
}