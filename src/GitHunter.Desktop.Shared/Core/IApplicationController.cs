using GitHunter.Desktop.Views;

namespace GitHunter.Desktop.Core;

public interface IApplicationController
{
    IViewMain ViewMain { get; }

    void ErrorMessage(string message);
    
    void SuccessMessage(string message);
    
    public IServiceProvider ServiceProvider { get; }

    void ShowGithubLogin();
    
    void StartApplication();
}