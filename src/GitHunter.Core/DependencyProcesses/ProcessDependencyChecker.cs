using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Core.DependencyProcesses;

public class ProcessDependencyChecker : IProcessDependencyChecker, ISingletonDependency
{
    private readonly IServiceProvider _serviceProvider;

    public ProcessDependencyChecker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public bool CheckDependency(Assembly assembly)
    {
        var dependenciesTypes = assembly.GetTypes().Where(t =>
            t.GetCustomAttributes(typeof(ProcessDependencyAttribute<>)) is { } attributes && attributes.Any());
        var dependenciesAttributes = dependenciesTypes
            .SelectMany(t => t.GetCustomAttributes(typeof(ProcessDependencyAttribute<>)));
        var dependencies = dependenciesAttributes.Cast<ProcessDependencyAttribute>().Select(t =>
        {
            if (t.DependencyProcess != null)
                return (IProcessDependency)ActivatorUtilities.CreateInstance(_serviceProvider, t.DependencyProcess);
            return null;
        });

        return dependencies.All(d => d != null && d.Check());
    }
}