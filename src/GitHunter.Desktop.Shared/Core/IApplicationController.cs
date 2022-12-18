namespace GitHunter.Desktop.Core;

public interface IApplicationController
{
    void StartApplication();
    public IServiceProvider ServiceProvider { get; }
}