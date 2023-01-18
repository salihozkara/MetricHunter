using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public interface IViewMainPresenter : IPresenter<IViewMain>
{
    IEnumerable<Repository> Repositories { get; set; }
    void LoadForm();
    void ShowGithubLogin();
    Task SearchRepositoriesAsync(CancellationToken cancellationToken = default);
    Task<string> CalculateMetricsAsync(CancellationToken cancellationToken = default);
    Task DownloadRepositoriesAsync(CancellationToken cancellationToken = default);
    Task ShowRepositoriesAsync(CancellationToken cancellationToken = default);
    Task SaveRepositoriesAsync(CancellationToken cancellationToken = default);
    Task<string> HuntRepositoriesAsync(CancellationToken cancellationToken = default);
}