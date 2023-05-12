using System.Diagnostics;
using MetricHunter.Application.Git;
using MetricHunter.Application.Metrics;
using MetricHunter.Application.Repositories;
using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public class ViewExploreRepositoriesPresenter : IViewExploreRepositoriesPresenter
{
    private readonly IApplicationController _controller;
    private readonly IMetricCalculatorManager _metricCalculatorManager;
    private List<RepositoryWithBranchNameDto> _repositories;
    private readonly IGitManager _gitManager;
    private readonly ILogger<ViewMainPresenter> _logger;

    public ViewExploreRepositoriesPresenter(IApplicationController controller, IViewExploreRepositories viewExploreRepositories)
    {
        _controller = controller;
        View = viewExploreRepositories;
        View.Presenter = this;
        
        _repositories = new List<RepositoryWithBranchNameDto>();

        _metricCalculatorManager = _controller.ServiceProvider.GetRequiredService<IMetricCalculatorManager>();
        
        _gitManager = _controller.ServiceProvider.GetRequiredService<IGitManager>();
        _gitManager.SearchRepositoriesRequestSuccess += GitManager_SearchRepositoriesRequestSuccess;

        _logger = _controller.ServiceProvider.GetRequiredService<ILogger<ViewMainPresenter>>();
    }
    
    public IViewExploreRepositories View { get; }
    
    public IEnumerable<RepositoryWithBranchNameDto> Repositories
    {
        get
        {
            return _controller.ViewMain.SelectedRepositories.Any()
                ? _repositories.Where(x => _controller.ViewMain.SelectedRepositories.Contains(x.Repository.Id.ToString())).ToList()
                : _repositories;
        }

        set => _repositories = value.ToList();
    }

    public async Task SearchRepositoriesAsync(CancellationToken cancellationToken = default)
    {
        _repositories.Clear();
        var gitInput = new GitInput
        {
            Language = View.SelectedLanguage,
            Order = View.SortDirection,
            Count = View.RepositoryCount,
            Topic = View.Topics
        };

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await _gitManager.GetRepositoriesAsync(gitInput, cancellationToken);
        _controller.SetProgressBar(100);
        stopwatch.Stop();

        _logger.LogInformation($"Search completed in {stopwatch.Elapsed:hh\\:mm\\:ss}");
    }
    
    public void LoadForm()
    {
        View.LanguageSelectList = _metricCalculatorManager.GetSupportedLanguages();
        View.SortDirectionSelectList = Enum.GetValues<SortDirection>().Reverse().ToList();
    }
    
    private void GitManager_SearchRepositoriesRequestSuccess(object? sender, RequestSuccessEventArgs e)
    {
        _repositories.AddRange(e.Result.Items.Select(x=> new RepositoryWithBranchNameDto(x)));
        _repositories = _repositories.DistinctBy(x => x.Repository.Id).Take(View.RepositoryCount).ToList();
        var progressBarValue = (int)Math.Round((double)_repositories.Count / View.RepositoryCount * 100);
        _controller.SetProgressBar(progressBarValue);
        _controller.ShowRepositories(Repositories);
        _controller.ViewMain.Presenter.Repositories = _repositories;
        View.Close();
    }

    public void Run()
    {
        View.Run();
    }
}