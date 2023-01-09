using MetricHunter.Core.Modules;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MetricHunter.Core;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpAutoMapperModule)
)]
public class CoreSharedModule : MetricHunterModule
{
}