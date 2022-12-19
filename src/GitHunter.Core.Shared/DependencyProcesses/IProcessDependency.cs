namespace GitHunter.Core.DependencyProcesses;

public interface IProcessDependency
{
    string ProcessName { get; }

    string ErrorMessage { get; }

    string ErrorTitle { get; }

    string? DownloadUrl { get; }
    bool Check();
}