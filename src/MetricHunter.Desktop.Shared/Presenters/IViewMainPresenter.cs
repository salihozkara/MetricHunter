using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public interface IViewMainPresenter : IPresenter<IViewMain>
{
    IEnumerable<Repository> Repositories { get; set; }
    void LoadForm();
    void ShowGithubLogin();
    Task SearchRepositories();
    Task<string> CalculateMetrics();
    Task DownloadRepositories();
    Task ShowRepositories();
    Task SaveRepositories();
    Task<string> HuntRepositories();
}