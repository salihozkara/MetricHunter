using GitHunter.Desktop.Core;
using GitHunter.Desktop.Presenters;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public class ApplicationController : IApplicationController, ISingletonDependency
{
    public ApplicationController(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public IServiceProvider ServiceProvider { get; }

    public void StartApplication()
    {
        using var viewMain = new ViewMain();
        var presenter = new ViewMainPresenter(viewMain, this);
        presenter.Run();
    }

    public void ShowGithubLogin()
    {
        using var viewGithubLogin = new ViewGithubLogin();
        var presenter = new ViewGithubLoginPresenter(this, viewGithubLogin);
        presenter.Run();
    }
}