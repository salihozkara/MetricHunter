using MetricHunter.Application.Repositories;
using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;

namespace MetricHunter.Desktop.Presenters;

public interface IViewExploreRepositoriesPresenter : IPresenter<IViewExploreRepositories>
{
    Task SearchRepositoriesAsync(CancellationToken cancellationToken = default);
    
    void LoadForm();
    
    IEnumerable<RepositoryWithBranchNameDto> Repositories { get; set; }
}