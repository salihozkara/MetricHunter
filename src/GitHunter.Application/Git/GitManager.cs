using GitHunter.Core.DependencyProcesses;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Application.Git;

[ProcessDependency<GitProcessDependency>]
public class GitManager : IGitManager, IDependencyProcess, ITransientDependency
{
    
}