using MetricHunter.Application.Git;
using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public interface IViewFindRepositoryPresenter : IPresenter<IViewFindRepository>
{
    public IGitManager GitManager { get; }

    Task FindRepository();
    
    Task GetCommits();
    
    Task GetReleases();
}