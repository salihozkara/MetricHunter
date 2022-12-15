namespace GitHunter.Core.Desktop;

public interface IApplicationController
{
    void StartApplication();
    public IServiceProvider ServiceProvider { get; }
}