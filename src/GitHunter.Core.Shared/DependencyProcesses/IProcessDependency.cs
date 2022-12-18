namespace GitHunter.Core.DependencyProcesses;

public interface IProcessDependency
{
    bool Check();
    
    string ProcessName { get; }
    
    string ErrorMessage { get; }
    
    string ErrorTitle { get; }
    
    string? DownloadUrl { get; }
}