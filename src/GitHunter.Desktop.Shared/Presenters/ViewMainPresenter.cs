using GitHunter.Application.Csv;
using GitHunter.Application.Git;
using GitHunter.Application.Metrics;
using GitHunter.Application.Resources;
using GitHunter.Desktop.Core;
using GitHunter.Desktop.Models;
using GitHunter.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Octokit;

namespace GitHunter.Desktop.Presenters;

public class ViewMainPresenter : IViewMainPresenter
{
    private readonly IApplicationController _controller;
    private readonly ICsvHelper _csvHelper;
    private readonly IGitManager _gitManager;
    private readonly IGitProvider _gitProvider;
    private readonly IMetricCalculatorManager _metricCalculatorManager;
    private readonly ILogger<ViewMainPresenter> _logger;
    private GitOutput? _gitOutput;

    public ViewMainPresenter(IViewMain view, IApplicationController controller)
    {
        View = view;
        _controller = controller;
        View.Presenter = this;

        _gitManager = _controller.ServiceProvider.GetRequiredService<IGitManager>();
        _gitProvider = _controller.ServiceProvider.GetRequiredService<IGitProvider>();
        _metricCalculatorManager = _controller.ServiceProvider.GetRequiredService<IMetricCalculatorManager>();

        _csvHelper = _controller.ServiceProvider.GetRequiredService<ICsvHelper>();
        _logger = _controller.ServiceProvider.GetRequiredService<ILogger<ViewMainPresenter>>();
        
        _gitProvider.CloneRepositorySuccess += OnCloneRepositorySuccess;
        _gitProvider.CloneRepositoryError += OnCloneRepositoryError;
    }

    private void OnCloneRepositoryError(object? sender, CloneRepositoryErrorEventArgs e)
    {
        _logger.LogError("{FullName} repository clone error: {Message}", e.Repository.FullName, e.Exception?.Message);
    }

    private void OnCloneRepositorySuccess(object? sender, CloneRepositorySuccessEventArgs e)
    {
        _logger.LogInformation("{FullName} cloned successfully", e.Repository.FullName);
    }

    public IViewMain View { get; }

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
        var gitInput = new GitInput
        {
            Language = View.SelectedLanguage,
            Order = View.SortDirection,
            Count = View.RepositoryCount,
            Topic = View.Topics
        };

        _gitOutput = await _gitManager.GetRepositories(gitInput);

        var repositoryModelList = _gitOutput.Repositories.Select(x => new RepositoryModel
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

    public async Task<string> CalculateMetrics()
    {
        if (View.SelectedLanguage is null || _gitOutput is null) return null;

        var manager = _metricCalculatorManager.FindMetricCalculator(View.SelectedLanguage.Value);

        var metrics = new List<Dictionary<string, string>>();
        foreach (var item in _gitOutput.Repositories)
        {
            var metric = await manager.CalculateMetricsAsync(item);
            var dictList = metric.ToDictionaryListByTopics();
            metrics.AddRange(dictList);
        }

        return _csvHelper.MetricsToCsv(metrics);
    }

    public async Task DownloadMetrics()
    {
        if (_gitOutput is null)
            return;

        var size = _gitOutput.Repositories.Sum(r => r.Size);
        var sizeInMb = size / 1024;
        var sizeInGb = sizeInMb / 1024;
        var sizeInTb = sizeInGb / 1024;
        var repositories = _gitOutput.Repositories.DistinctBy(r => r.CloneUrl).ToList();
        foreach (var item in repositories)
        {
            await _gitProvider.CloneRepository(item);
        }
    }

    public void LoadRepositoriesFromFiles(string path)
    {
        _gitOutput = new GitOutput(JsonConvert.DeserializeObject<List<Repository>>(
            File.ReadAllText(path), Resource.Jsons.JsonSerializerSettings)!, Array.Empty<SearchRepositoriesRequest>());
    }
}