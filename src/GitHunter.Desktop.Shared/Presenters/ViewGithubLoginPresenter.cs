using GitHunter.Desktop.Core;
using GitHunter.Desktop.Views;

namespace GitHunter.Desktop.Presenters;

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