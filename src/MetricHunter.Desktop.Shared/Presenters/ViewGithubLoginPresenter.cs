using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;

namespace MetricHunter.Desktop.Presenters;

public class ViewGithubLoginPresenter : IViewGithubLoginPresenter
{
    private readonly IApplicationController _controller;

    public ViewGithubLoginPresenter(IApplicationController controller, IViewGithubLogin viewGithubLogin)
    {
        _controller = controller;
        View = viewGithubLogin;
        View.Presenter = this;
    }

    public IViewGithubLogin View { get; }

    public void Run()
    {
        View.Run();
    }
}