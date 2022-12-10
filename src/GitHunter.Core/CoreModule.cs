using GitHunter.Core.DependencyProcesses;
using GitHunter.Core.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace GitHunter.Core;

[DependsOn(typeof(CoreSharedModule))]
public class CoreModule : GitHunterModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // var services = context.Services;
        // // var processDependencyChecker = services.GetRequiredService<ProcessDependencyChecker>();
        // var processDependencyOptions = services.GetRequiredService<IOptions<ProcessDependencyOptions>>().Value;
        //
        // var modules = services.GetSingletonInstance<IModuleLoader>()
        //     .LoadModules(
        //         services,
        //         processDependencyOptions.StartupModule,
        //         new AbpApplicationCreationOptions(context.Services).PlugInSources
        //     );
        
        
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
        using (var scope = context.ServiceProvider.CreateScope())
        {
            var processDependencyChecker = scope.ServiceProvider.GetRequiredService<IProcessDependencyChecker>();
            var processDependencyOptions = scope.ServiceProvider.GetRequiredService<IOptions<ProcessDependencyOptions>>().Value;
            var modules = ModuleHelper.FindGitHunterModuleTypes(processDependencyOptions.StartupModule);
                
            foreach (var assembly in modules.Select(m=>m.Assembly))
            {
                processDependencyChecker?.CheckDependency(assembly);
            }
        }
        
        base.OnApplicationInitialization(context);
    }
}