using GitHunter.Desktop.Core;
using GitHunter.Desktop.Views;
using Octokit;

namespace GitHunter.Desktop.Presenters;

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
}