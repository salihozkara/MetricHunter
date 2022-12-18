using GitHunter.Application.Git;
using GitHunter.Application.Metrics;
using GitHunter.Desktop.Core;
using GitHunter.Desktop.Models;
using GitHunter.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace GitHunter.Desktop.Presenters;

public class ViewMainPresenter : IViewMainPresenter
{
    private readonly IApplicationController _controller;
    private readonly IGitManager _gitManager;
    private readonly IGitProvider _gitProvider;
    private readonly IMetricCalculatorManager _metricCalculatorManager;
    private GitOutput? _gitOutput;

    public IViewMain View { get; }

    public ViewMainPresenter(IViewMain view, IApplicationController controller)
    {
        View = view;
        _controller = controller;
        View.Presenter = this;
        
        _gitManager = _controller.ServiceProvider.GetRequiredService<IGitManager>();
        _gitProvider = _controller.ServiceProvider.GetRequiredService<IGitProvider>();
        _metricCalculatorManager = _controller.ServiceProvider.GetRequiredService<IMetricCalculatorManager>();
    }

    public void Run()
    {
        View.Run();
    }

    public void LoadForm()
    {
        View.LanguageSelectList = _metricCalculatorManager.GetSupportedLanguages();
        View.SortDirectionSelectList = Enum.GetValues<SortDirection>();
    }

    public async Task SearchRepositories()
    {
        var gitInput = new GitInput()
        {
            Language = View.SelectedLanguage,
            Order = View.SortDirection,
            Count = View.RepositoryCount,
            Topic = View.Topics
        };
        
        _gitOutput = await _gitManager.GetRepositories(gitInput);
        
        var repositoryModelList = _gitOutput.Repositories.Select(x => new RepositoryModel()
        {
            Name = x.Name,
            Description = x.Description,
            Stars = x.StargazersCount,
            Url = x.HtmlUrl,
            License = x.License?.Name ?? "Lisans Yok",
            Owner = x.Owner.Login
        }).ToList();
        
        View.ShowRepositories(repositoryModelList);
    }

    public async Task CalculateMetrics()
    {
        if (View.SelectedLanguage is null || _gitOutput is null) return;

        var manager = _metricCalculatorManager.FindMetricCalculator(View.SelectedLanguage.Value);
        
        foreach (var item in _gitOutput.Repositories)
        {
            await manager.CalculateMetricsAsync(item);
        }
    }

    public async Task DownloadMetrics()
    {
        if (_gitOutput is  null) 
            return;
        
        foreach (var item in _gitOutput.Repositories)
        {
            await _gitProvider.CloneRepository(item);
        }
    }
}