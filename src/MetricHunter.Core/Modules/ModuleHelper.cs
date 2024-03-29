﻿using System.Reflection;
using Volo.Abp.Modularity;

namespace MetricHunter.Core.Modules;

internal static class ModuleHelper
{
    public static IEnumerable<Type> FindMetricHunterModuleTypes(Type startupModuleType)
    {
        if (!MetricHunterModule.IsMetricHunterModule(startupModuleType))
            throw new ArgumentException("Given type is not an MetricHunter module: " +
                                        startupModuleType.AssemblyQualifiedName);
        var moduleTypes = new List<Type>();
        AddModuleAndDependenciesRecursively(moduleTypes, startupModuleType);
        return moduleTypes;
    }

    private static List<Type> FindDependedModuleTypes(ICustomAttributeProvider moduleType)
    {
        var dependencies = new List<Type>();

        var dependencyDescriptors = moduleType
            .GetCustomAttributes(false)
            .OfType<IDependedTypesProvider>();

        foreach (var descriptor in dependencyDescriptors)
        foreach (var dependedModuleType in descriptor.GetDependedTypes())
            dependencies.AddIfNotContains(dependedModuleType);

        return dependencies;
    }

    private static void AddModuleAndDependenciesRecursively(
        ICollection<Type> moduleTypes,
        Type moduleType)
    {
        if (!MetricHunterModule.IsMetricHunterModule(moduleType))
            return;

        if (moduleTypes.Contains(moduleType)) return;


        moduleTypes.Add(moduleType);

        foreach (var dependedModuleType in FindDependedModuleTypes(moduleType))
            AddModuleAndDependenciesRecursively(moduleTypes, dependedModuleType);
    }
}