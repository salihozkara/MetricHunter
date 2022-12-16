using GitHunter.Application.Git;
using GitHunter.Desktop.Core;
using GitHunter.Desktop.Presenters;
using GitHunter.Desktop.Views;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public class ApplicationController : IApplicationController, ISingletonDependency
{
    public IServiceProvider ServiceProvider { get; }

    public ApplicationController(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
    
    public void StartApplication()
    {
        using var viewMain = new ViewMain();
        var presenter = new ViewMainPresenter(viewMain, this);
        presenter.Run();
    }
}