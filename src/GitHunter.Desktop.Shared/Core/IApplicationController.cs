namespace GitHunter.Desktop.Core;

public interface IApplicationController
{
    public IServiceProvider ServiceProvider { get; }

    void ShowGithubLogin();
    
    void StartApplication();
}