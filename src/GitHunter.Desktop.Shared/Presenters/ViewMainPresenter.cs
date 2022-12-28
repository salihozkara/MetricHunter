using GitHunter.Application.Csv;
using GitHunter.Application.Git;
using GitHunter.Application.Metrics;
using GitHunter.Application.Resources;
using GitHunter.Desktop.Core;
using GitHunter.Desktop.Models;
using GitHunter.Desktop.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Octokit;
using System.Text;
using GitHunter.Application.Repositories;
using Volo.Abp;
using FileMode = System.IO.FileMode;

namespace GitHunter.Desktop.Presenters;

public class ViewMainPresenter : IViewMainPresenter
{
    private readonly IApplicationController _controller;
    private readonly ICsvHelper _csvHelper;
    private readonly IGitManager _gitManager;
    private readonly IGitProvider _gitProvider;
    private readonly IMetricCalculatorManager _metricCalculatorManager;
    private IEnumerable<Repository> _repositories;
    private IRepositoryAppService _repositoryAppService;

    public IEnumerable<Repository> Repositories
    {
        get
        {
            return View.SelectedRepositories.Any()
                ? _repositories.Where(x => View.SelectedRepositories.Contains(x.Id)).ToList()
                : _repositories;
        }
        
        set => _repositories = value;
    }

    public ViewMainPresenter(IViewMain view, IApplicationController controller)
    {
        View = view;
        _controller = controller;
        View.Presenter = this;

        _repositories = new List<Repository>();

        _gitManager = _controller.ServiceProvider.GetRequiredService<IGitManager>();
        _gitProvider = _controller.ServiceProvider.GetRequiredService<IGitProvider>();
        _metricCalculatorManager = _controller.ServiceProvider.GetRequiredService<IMetricCalculatorManager>();
        _csvHelper = _controller.ServiceProvider.GetRequiredService<ICsvHelper>();
        _repositoryAppService = _controller.ServiceProvider.GetRequiredService<IRepositoryAppService>();
    }

    public IViewMain View { get; }

    public void Run()
    {
        View.Run();
    }

    public void LoadForm()
    {
        View.LanguageSelectList = _metricCalculatorManager.GetSupportedLanguages();
        View.SortDirectionSelectList = Enum.GetValues<SortDirection>().Reverse().ToList();
    }

    public void ShowGithubLogin()
    {
        _controller.ShowGithubLogin();
    }

    public async Task SearchRepositories()
    {
        var gitInput = new GitInput
        {
            Language = View.SelectedLanguage,
            Order = View.SortDirection,
            Count = View.RepositoryCount,
            Topic = View.Topics
        };

        var gitResult = await _gitManager.GetRepositories(gitInput);

        _repositories = gitResult.Repositories;

        var repositoryModelList = Repositories.Select(x => new RepositoryModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Stars = x.StargazersCount,
            Url = x.HtmlUrl,
            License = x.License?.Name ?? "Lisans Yok",
            Owner = x.Owner.Login
        }).ToList();

        View.ShowRepositories(repositoryModelList);
    }

    public async Task<string> CalculateMetrics()
    {
        if (!Repositories.Any())
        {
            _controller.ErrorMessage("Repository bulunamadı");
        }

        if (View.SelectedLanguage is null) return null;

        var manager = _metricCalculatorManager.FindMetricCalculator(View.SelectedLanguage.Value);

        var metrics = new List<Dictionary<string, string>>();
        foreach (var item in Repositories)
        {
            var metric = await manager.CalculateMetricsAsync(item);
            var dictList = metric.ToDictionaryListByTopics();
            metrics.AddRange(dictList);
        }

        return _csvHelper.MetricsToCsv(metrics);
    }

    public async Task DownloadRepositories()
    {
        if (!Repositories.Any())
        {
            _controller.ErrorMessage("Repository bulunamadı.");
            return;
        }
        
        foreach (var item in Repositories) await _gitProvider.CloneRepository(item, View.DownloadRepositoryPath);
    }

    public async Task ShowRepositories()
    {
        if (View.JsonLoadPath.IsNullOrEmpty())
        {
            _controller.ErrorMessage("Dosya bulunamadı");
            return;
        }

        var repositories = await _repositoryAppService.ReadRepositories(View.JsonLoadPath);

        if (!repositories.Any()) return;

        Repositories = repositories;

        var repositoryModelList = Repositories.Select(x => new RepositoryModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Stars = x.StargazersCount,
            Url = x.HtmlUrl,
            License = x.License?.Name ?? "No Licence",
            Owner = x.Owner.Login
        }).ToList();

        View.ShowRepositories(repositoryModelList);
    }

    public async Task SaveRepositories()
    {
        if (!Repositories.Any())
        {
            _controller.ErrorMessage("No repositories found for save.");
            return;
        }

        await _repositoryAppService.WriteRepositories(Repositories, View.JsonSavePath);
    }
}