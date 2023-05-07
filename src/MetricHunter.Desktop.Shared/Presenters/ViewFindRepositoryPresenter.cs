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
        _repository = await _gitManager.GetRepositoryAsync(View.RepositoryFullNameOrUrl);

        if (_repository is null)
        {
            _controller.ErrorMessage("Repository not found");
        }
        else
        {
            View.ShowRepository(_repository);
        }
    }

    public void Run()
    {
        View.Run();
    }

}