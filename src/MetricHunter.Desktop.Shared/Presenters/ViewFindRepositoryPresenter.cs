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

    private Repository _repository;
    
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
            _repository = await GitManager.GetRepositoryAsync(View.RepositoryFullNameOrUrl);
            View.ShowRepository(_repository);
        }
        catch (Exception e)
        {
            _controller.ErrorMessage("Repository not found");
        }
    }

    public async Task GetCommits()
    {
        var commits = await GitManager.GetCommitsAsync(_repository.FullName);

        _controller.ShowRepositories(commits.Select(x => new RepositoryWithBranchNameDto(_repository, x.Sha, x))
            .ToList());
    }

    public async Task GetReleases()
    {
        var releases = await GitManager.GetReleasesAsync(_repository.FullName);

        _controller.ShowRepositories(releases.Select(x => new RepositoryWithBranchNameDto(_repository, x.TagName, x))
            .ToList());
    }

    public void Run()
    {
        View.Run();
    }

}