using System.Reflection;
using Volo.Abp.Modularity;

namespace MetricHunter.Core.Modules;

public class MetricHunterModule : AbpModule
{
    public static bool IsMetricHunterModule(Type type)
    {
        var typeInfo = type.GetTypeInfo();

        return
            typeInfo.IsClass &&
            !typeInfo.IsAbstract &&
            !typeInfo.IsGenericType &&
            typeof(MetricHunterModule).GetTypeInfo().IsAssignableFrom(type);
    }
}