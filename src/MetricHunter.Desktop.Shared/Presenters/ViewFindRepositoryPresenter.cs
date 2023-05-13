using MetricHunter.Application.Git;
using MetricHunter.Application.Repositories;
using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public class ViewFindRepositoryPresenter : IViewFindRepositoryPresenter
{
    private readonly IApplicationController _controller;
    public IGitManager GitManager { get; }

    public Repository Repository { get; set; }
    
    public ViewFindRepositoryPresenter(IApplicationController controller, IViewFindRepository viewFindRepository)
    {
        _controller = controller;
        View = viewFindRepository;
        View.Presenter = this;

        GitManager = _controller.ServiceProvider.GetRequiredService<IGitManager>();
    }
    
    public IViewFindRepository View { get; }
    
    public async Task FindRepository()
    {
        try
        {
            Repository = await GitManager.GetRepositoryAsync(View.RepositoryFullNameOrUrl);
            View.ShowRepository(Repository);
        }
        catch (Exception e)
        {
            _controller.ErrorMessage("Repository not found");
        }
    }

    public async Task GetCommits()
    {
        var commits = await GitManager.GetCommitsAsync(Repository.FullName);

        _controller.ShowRepositories(commits.Select(x => new RepositoryWithBranchNameDto(Repository, x.Sha, x))
            .ToList());
    }

    public async Task GetReleases()
    {
        var releases = await GitManager.GetReleasesAsync(Repository.FullName);

        _controller.ShowRepositories(releases.Select(x => new RepositoryWithBranchNameDto(Repository, x.TagName, x))
            .ToList());
    }

    public void Run()
    {
        View.Run();
    }

}