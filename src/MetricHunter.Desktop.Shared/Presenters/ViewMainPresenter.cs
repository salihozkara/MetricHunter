using System.Diagnostics;
using MetricHunter.Application.Csv;
using MetricHunter.Application.Git;
using MetricHunter.Application.Metrics;
using MetricHunter.Application.Repositories;
using MetricHunter.Application.Resources;
using MetricHunter.Core.Paths;
using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public class ViewMainPresenter : IViewMainPresenter
{
    private readonly IApplicationController _controller;
    private readonly ICsvHelper _csvHelper;
    private readonly IGitManager _gitManager;
    private readonly IGitProvider _gitProvider;
    private readonly IMetricCalculatorManager _metricCalculatorManager;
    private readonly IRepositoryAppService _repositoryAppService;
    private List<Repository> _repositories;

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
        
        Authenticate();

        _gitManager.SearchRepositoriesRequestSuccess += GitManager_SearchRepositoriesRequestSuccess;
    }

    private void Authenticate()
    {
        if (!string.IsNullOrWhiteSpace(View.GithubToken))
            _gitManager.Authenticate(View.GithubToken);
    }

    private void GitManager_SearchRepositoriesRequestSuccess(object? sender, RequestSuccessEventArgs e)
    {
        _repositories.AddRange(e.Result.Items);
        _repositories = _repositories.DistinctBy(x=>x.Id).Take(View.RepositoryCount).ToList();
        var progressBarValue = (int) Math.Round((double) _repositories.Count / View.RepositoryCount * 100);
        View.SetSearchProgressBar(progressBarValue);
        View.ShowRepositories(Repositories);
    }

    public IEnumerable<Repository> Repositories
    {
        get
        {
            return View.SelectedRepositories.Any()
                ? _repositories.Where(x => View.SelectedRepositories.Contains(x.Id)).ToList()
                : _repositories;
        }

        set
        {
            _repositories = value.ToList();
            View.ShowRepositories(_repositories);
        }
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
        Authenticate();
    }

    public async Task SearchRepositories()
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
        await _gitManager.GetRepositoriesAsync(gitInput);
        stopwatch.Stop();
        View.ShowRepositories(Repositories);
        
        View.ShowMessage($"Search completed in {stopwatch.Elapsed:hh\\:mm\\:ss}");
    }
    

    public async Task<string> CalculateMetrics()
    {
        var repositoryList = Repositories.ToList();
        var directoryInfo = new DirectoryInfo(View.CalculateMetricsRepositoryPath);
        if (!repositoryList.Any())
        {
            var infoFiles = directoryInfo.GetFiles("*"+GitConsts.RepositoryInfoFileExtension, SearchOption.AllDirectories);
            repositoryList = infoFiles.Select(x => JsonConvert.DeserializeObject<Repository>(File.ReadAllText(x.FullName),Resource.Jsons.JsonSerializerSettings)).ToList();
        }

        var metrics = new List<Dictionary<string, string>>();
        foreach (var item in repositoryList)
        {
            var language = GitConsts.LanguagesMap[item.Language];
            var manager = _metricCalculatorManager.FindMetricCalculator(language);
            var metric = await manager.CalculateMetricsAsync(item, View.CalculateMetricsRepositoryPath, View.CalculateMetricsByLocalResultsPath);
            if (metric.IsEmpty())
                continue;
            var dictList = metric.ToDictionaryListByTopics();
            metrics.AddRange(dictList);
        }
        return _csvHelper.MetricsToCsv(metrics);
    }

    public async Task DownloadRepositories()
    {
        if (!CheckSelectRepositories())
            return;

        foreach (var item in Repositories) await _gitProvider.CloneRepository(item, View.DownloadRepositoryPath);
    }

    public async Task ShowRepositories()
    {
        if (View.JsonLoadPath.IsNullOrEmpty())
        {
            _controller.ErrorMessage("No file selected");
            return;
        }

        var repositories = await _repositoryAppService.ReadRepositories(View.JsonLoadPath);

        if (!repositories.Any()) return;

        Repositories = repositories;

        View.ShowRepositories(Repositories);
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

    public async Task<string> HuntRepositories()
    {
        if (!CheckSelectRepositories())
            return string.Empty;

        var metrics = new List<Dictionary<string, string>>();
        foreach (var item in Repositories)
            if (await _gitProvider.CloneRepository(item))
            {
                var language = GitConsts.LanguagesMap[item.Language];
                var manager = _metricCalculatorManager.FindMetricCalculator(language);
                var metric = await manager.CalculateMetricsAsync(item);
                var dictList = metric.ToDictionaryListByTopics();
                metrics.AddRange(dictList);
                await _gitProvider.DeleteLocalRepository(item);
            }

        return _csvHelper.MetricsToCsv(metrics);
    }

    private bool CheckSelectRepositories()
    {
        if (!Repositories.Any())
        {
            _controller.ErrorMessage("No repositories selected");
            return false;
        }

        return true;
    }
}