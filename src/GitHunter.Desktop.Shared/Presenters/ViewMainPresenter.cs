using GitHunter.Application.Git;
using GitHunter.Application.LanguageStatistics;
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
    private readonly IMetricCalculatorManager _metricCalculatorManager;
    private GitOutput? _gitOutput;

    public IViewMain View { get; }

    public ViewMainPresenter(IViewMain view, IApplicationController controller)
    {
        View = view;
        _controller = controller;
        View.Presenter = this;
        
        _gitManager = _controller.ServiceProvider.GetRequiredService<IGitManager>();
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
}