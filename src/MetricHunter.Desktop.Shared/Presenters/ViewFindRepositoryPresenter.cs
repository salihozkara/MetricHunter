using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;

namespace MetricHunter.Desktop.Presenters;

public class ViewFindRepositoryPresenter : IViewFindRepositoryPresenter
{
    private readonly IApplicationController _controller;

    public ViewFindRepositoryPresenter(IApplicationController controller, IViewFindRepository viewFindRepository)
    {
        _controller = controller;
        View = viewFindRepository;
        View.Presenter = this;
    }
    
    public IViewFindRepository View { get; }

    public void Run()
    {
        View.Run();
    }

}