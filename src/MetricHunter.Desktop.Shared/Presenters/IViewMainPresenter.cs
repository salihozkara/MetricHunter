using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public interface IViewMainPresenter : IPresenter<IViewMain>
{
    public Repository FoundRepository { get; set; }
    
    public IEnumerable<Repository> Repositories { get; set; }
    
    Task<string> CalculateMetricsAsync(CancellationToken cancellationToken = default);
    
    Task DownloadRepositoriesAsync(CancellationToken cancellationToken = default);
    
    Task ShowRepositoriesAsync(CancellationToken cancellationToken = default);
    
    Task SaveRepositoriesAsync(CancellationToken cancellationToken = default);
    
    Task<string> HuntRepositoriesAsync(CancellationToken cancellationToken = default);
    
    void ShowGithubLogin();
    
    void ShowExploreRepositories();
    
    void ShowFindRepository();

    Task DownloadCommitsAsync(CancellationToken token = default);
    
    Task DownloadReleasesAsync(CancellationToken token = default);
}