using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Presenters;
using Octokit;

namespace MetricHunter.Desktop.Views;

public interface IViewFindRepository : IView<IViewFindRepositoryPresenter>
{
    string RepositoryFullNameOrUrl { get; }
    
    void ShowRepository(Repository repository);
}