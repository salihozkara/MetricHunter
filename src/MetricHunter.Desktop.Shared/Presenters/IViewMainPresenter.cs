using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public interface IViewMainPresenter : IPresenter<IViewMain>
{
    Task<string> CalculateMetricsAsync(CancellationToken cancellationToken = default);
    
    Task DownloadRepositoriesAsync(CancellationToken cancellationToken = default);
    
    Task ShowRepositoriesAsync(CancellationToken cancellationToken = default);
    
    Task SaveRepositoriesAsync(CancellationToken cancellationToken = default);
    
    Task<string> HuntRepositoriesAsync(CancellationToken cancellationToken = default);
    
    void ShowGithubLogin();
    
    void ShowExploreRepositories();
    
    void ShowFindRepository();

}