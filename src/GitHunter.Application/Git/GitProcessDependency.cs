namespace GitHunter.Application.Git;

public class GitProcessDependency : IGitProcessDependency
{
    public bool Check()
    {
        var environmentVariable = Environment.GetEnvironmentVariable("PATH");
        var paths = environmentVariable?.Split(';');
        return paths != null && paths.Any(path => File.Exists(Path.Combine(path, ProcessName)));
    }

    public string ProcessName => "git.exe";
    public string ErrorMessage => "Git is not installed. Please download and install Git from " + DownloadUrl;
    public string ErrorTitle => "Git not found";
    public string DownloadUrl => "https://git-scm.com/downloads";
}