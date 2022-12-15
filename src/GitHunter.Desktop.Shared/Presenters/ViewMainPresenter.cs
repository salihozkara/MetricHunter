using GitHunter.Application.Git;
using GitHunter.Core.Desktop;
using GitHunter.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;

namespace GitHunter.Desktop.Presenters;

public class ViewMainPresenter : IViewMainPresenter
{
    private readonly IApplicationController _controller;
    private readonly IGitManager _gitManager;
    private GitOutput? _gitOutput;

    public IViewMain View { get; }

    public ViewMainPresenter(IViewMain view, IApplicationController controller)
    {
        View = view;
        _controller = controller;
        View.Presenter = this;
        
        _gitManager = _controller.ServiceProvider.GetRequiredService<IGitManager>();
    }

    public void Run()
    {
        View.Run();
    }

    public async Task SearchRepositories()
    {
        var gitInput = new GitInput()
        {
            Language = View.SelectedLanguage,
            Order = View.SortDirection,
            Count = View.RepositoryCount
        };

        _gitOutput = await _gitManager.GetRepositories(gitInput);
        View.ShowRepositories(_gitOutput.Repositories);
    }
}