using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Presenters;
using Octokit;
using Language = MetricHunter.Core.Languages.Language;

namespace MetricHunter.Desktop.Views;

public interface IViewMain : IView<IViewMainPresenter>
{
    Mode Mode { get; set; }
    string GithubToken { get; }
    
    IEnumerable<long> SelectedRepositories { get; }
    
    IEnumerable<string> SelectedCommits { get; }

    IEnumerable<string> SelectedReleases { get; }
    
    string JsonLoadPath { get; set; }

    string JsonSavePath { get; set; }

    string DownloadRepositoryPath { get; set; }
    
    string CalculateMetricsRepositoryPath { get; set; }
    
    string CalculateMetricsByLocalResultsPath { get; set; }
    
    CancellationTokenSource CancellationTokenSource { get; set; }
    
    void ShowMessage(string message);

    void ShowRepositories(IEnumerable<Repository> repositories);

    void SetProgressBar(int value);
    
    void ShowCommits(IEnumerable<GitHubCommit> gitHubCommits);
    
    void ShowReleases(IEnumerable<Release> releases);
}