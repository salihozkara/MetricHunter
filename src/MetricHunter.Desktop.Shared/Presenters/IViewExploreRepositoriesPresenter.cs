using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public interface IViewExploreRepositoriesPresenter : IPresenter<IViewExploreRepositories>
{
    Task SearchRepositoriesAsync(CancellationToken cancellationToken = default);
    
    void LoadForm();
    
    IEnumerable<Repository> Repositories { get; set; }
}