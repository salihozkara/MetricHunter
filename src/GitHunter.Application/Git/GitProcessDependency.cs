namespace GitHunter.Application.Git;

public class GitProcessDependency : IGitProcessDependency
{
    public bool Check()
    {
        var environmentVariable = Environment.GetEnvironmentVariable("PATH");
        var paths = environmentVariable?.Split(';');
        return paths != null && paths.Any(path => File.Exists(Path.Combine(path, "git.exe")));
    }
}