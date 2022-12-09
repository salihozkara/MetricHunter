using GitHunter.Core.Shared;
using Volo.Abp.Modularity;

namespace GitHunter.Application.Shared;

[DependsOn(typeof(CoreSharedModule))]
public class ApplicationSharedModule : AbpModule
{
}