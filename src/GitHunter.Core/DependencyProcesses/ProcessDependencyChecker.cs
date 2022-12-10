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
        var dependenciesTypes = assembly.GetTypes().Where(t => typeof(IDependencyProcess).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList();
        var dependenciesAttributes = dependenciesTypes.SelectMany(t => t.GetCustomAttributes(typeof(ProcessDependencyAttribute<>))).ToList();
        var dependencies = dependenciesAttributes.Cast<ProcessDependencyAttribute>().Select(t=>(IProcessDependency)ActivatorUtilities.CreateInstance(_serviceProvider, t.DependencyProcess)).ToList();
       
        return dependencies.All(d => d.Check());
    }
}