using MetricHunter.Desktop.Views;

namespace MetricHunter.Desktop.Core;

public interface IApplicationController
{
    IViewMain ViewMain { get; }

    void ErrorMessage(string message);
    
    void SuccessMessage(string message);
    
    public IServiceProvider ServiceProvider { get; }

    void ShowGithubLogin();
    
    void StartApplication();
}