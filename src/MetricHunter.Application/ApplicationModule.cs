using MetricHunter.Core;
using MetricHunter.Core.Modules;
using Volo.Abp.Modularity;

namespace MetricHunter.Application;

[DependsOn(
    typeof(ApplicationSharedModule),
    typeof(CoreModule)
)]
public class ApplicationModule : MetricHunterModule
{
}