using MetricHunter.Application.Repositories;
using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Forms;
using MetricHunter.Desktop.Presenters;
using MetricHunter.Desktop.Views;
using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Desktop;

public class ApplicationController : IApplicationController, ISingletonDependency
{
    private readonly ILogger<ViewMain> _logger;
    private ViewFindRepository _viewFindRepository;
    private ViewMain _viewMain;

    public ApplicationController(IServiceProvider serviceProvider, IViewMain viewMain, ILogger<ViewMain> logger)
    {
        _logger = logger;
        ServiceProvider = serviceProvider;
        ViewMain = viewMain;
        _viewMain = (ViewMain) viewMain;
    }

    public IViewMain ViewMain { get; }
    

    public void ErrorMessage(string message)
    {
        MessageBox.Show(message);
        _logger.LogError(message);
    }

    public void SuccessMessage(string message)
    {
        MessageBox.Show(message);
    }

    public IServiceProvider ServiceProvider { get; }

    public void StartApplication()
    {
        var presenter = new ViewMainPresenter(ViewMain, this);
        presenter.Run();
    }

    public void ShowExploreRepositories()
    {
        using var viewExploreRepositories = new ViewExploreRepositories();
        var presenter = new ViewExploreRepositoriesPresenter(this, viewExploreRepositories);
        presenter.Run();
    }

    public void ShowFindRepository()
    {
        _viewFindRepository = new ViewFindRepository();
        var presenter = new ViewFindRepositoryPresenter(this, _viewFindRepository);
        presenter.Run();
    }

    public void ShowRepositories(IEnumerable<RepositoryWithBranchNameDto> repositories)
    {
        ViewMain.Presenter.Repositories = repositories;
    }

    public void ExploreRepository(Repository repository)
    {
        _viewFindRepository = new ViewFindRepository();
        var presenter = new ViewFindRepositoryPresenter(this, _viewFindRepository);
        presenter.Run();
        presenter.View.ShowRepository(repository);
    }

    public void SetProgressBar(int i)
    {
        ViewMain.SetProgressBar(i);
    }

    public void ShowGithubLogin()
    {
        using var viewGithubLogin = new ViewGithubLogin();
        var presenter = new ViewGithubLoginPresenter(this, viewGithubLogin);
        presenter.Run();
    }
    
}