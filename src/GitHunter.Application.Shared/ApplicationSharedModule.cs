using GitHunter.Core;
using GitHunter.Core.Modules;
using Volo.Abp.Modularity;

namespace GitHunter.Application;

[DependsOn(typeof(CoreSharedModule))]
public class ApplicationSharedModule : GitHunterModule
{
}