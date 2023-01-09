using MetricHunter.Core;
using MetricHunter.Core.Modules;
using Volo.Abp.Modularity;

namespace MetricHunter.Application;

[DependsOn(typeof(CoreSharedModule))]
public class ApplicationSharedModule : MetricHunterModule
{
}