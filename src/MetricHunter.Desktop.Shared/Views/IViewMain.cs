using MetricHunter.Application.Repositories;
using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Presenters;
using Octokit;

namespace MetricHunter.Desktop.Views;

public interface IViewMain : IView<IViewMainPresenter>
{
    string GithubToken { get; }
    
    IEnumerable<string> SelectedRepositories { get; }

    string JsonLoadPath { get; set; }

    string JsonSavePath { get; set; }

    string DownloadRepositoryPath { get; set; }
    
    string CalculateMetricsRepositoryPath { get; set; }
    
    string CalculateMetricsByLocalResultsPath { get; set; }
    
    CancellationTokenSource CancellationTokenSource { get; set; }
    
    void ShowMessage(string message);

    void ShowRepositories(IEnumerable<RepositoryWithBranchNameDto> repositories);

    void SetProgressBar(int value);
    
    void ShowCommits(IEnumerable<GitHubCommit> gitHubCommits);
    
    void ShowReleases(IEnumerable<Release> releases);
    
    void CompleteRepository(string id);
}