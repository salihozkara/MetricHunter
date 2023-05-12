using MetricHunter.Application.Repositories;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Core;

public interface IApplicationController
{
    IViewMain ViewMain { get; }

    public IServiceProvider ServiceProvider { get; }

    void ErrorMessage(string message);

    void SuccessMessage(string message);

    void ShowGithubLogin();

    void StartApplication();
    void ShowExploreRepositories();
    void ShowFindRepository();
    
    void ShowRepositories(IEnumerable<RepositoryWithBranchNameDto> repositories);
    
    void SetProgressBar(int i);
}