using GitHunter.Core.DependencyProcesses;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Core.Git;

[ProcessDependency<GitProcessDependency>]
public class GitManager : IGitManager, IDependencyProcess, ITransientDependency
{
    
}