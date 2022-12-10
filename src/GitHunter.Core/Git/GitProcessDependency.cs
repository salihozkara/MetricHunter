using GitHunter.Core.DependencyProcesses;

namespace GitHunter.Core.Git;

public class GitProcessDependency : IProcessDependency
{
    public bool Check()
    {
        var environmentVariable = Environment.GetEnvironmentVariable("PATH");
        var paths = environmentVariable?.Split(';');
        return paths != null && paths.Any(path => File.Exists(Path.Combine(path, "git.exe")));
    }
}