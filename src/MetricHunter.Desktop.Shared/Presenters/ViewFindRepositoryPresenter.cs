using MetricHunter.Application.Git;
using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public class ViewFindRepositoryPresenter : IViewFindRepositoryPresenter
{
    private readonly IApplicationController _controller;
    private readonly IGitManager _gitManager;
    
    private Repository _repository;
    
    public ViewFindRepositoryPresenter(IApplicationController controller, IViewFindRepository viewFindRepository)
    {
        _controller = controller;
        View = viewFindRepository;
        View.Presenter = this;
        
        _gitManager = _controller.ServiceProvider.GetRequiredService<IGitManager>();
    }
    
    public IViewFindRepository View { get; }
    
    public async Task FindRepository()
    {
        try
        {
            _repository = await _gitManager.GetRepositoryAsync(View.RepositoryFullNameOrUrl);
            View.ShowRepository(_repository);
        }
        catch (Exception e)
        {
            _controller.ErrorMessage("Repository not found");
        }
    }

    public async Task GetCommits()
    {
        var commits = await _gitManager.GetCommitsAsync(_repository.FullName);

        _controller.ShowCommits(_repository, commits);
    }

    public async Task GetReleases()
    {
        var releases = await _gitManager.GetReleasesAsync(_repository.FullName);

        _controller.ShowReleases(_repository, releases);
    }

    public void Run()
    {
        View.Run();
    }

}