using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public interface IViewFindRepositoryPresenter : IPresenter<IViewFindRepository>
{
    Task FindRepository();
    
    Task GetCommits();
    
    Task GetReleases();
}