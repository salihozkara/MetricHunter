using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;

namespace MetricHunter.Desktop.Presenters;

public class ViewGithubLoginPresenter : IViewGithubLoginPresenter
{
    private readonly IApplicationController _controller;
    public IViewGithubLogin View { get; }
    
    public ViewGithubLoginPresenter(IApplicationController controller, IViewGithubLogin viewGithubLogin)
    {
        _controller = controller;
        View = viewGithubLogin;
    }
    
    public void Run()
    {
        View.Run();
    }
}