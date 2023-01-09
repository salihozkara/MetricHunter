using MetricHunter.Application;
using MetricHunter.Core.Modules;
using Volo.Abp.Modularity;

namespace MetricHunter.Desktop;

[DependsOn(
    typeof(ApplicationSharedModule)
)]
public class DesktopSharedModule : MetricHunterModule
{
}