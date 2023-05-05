using System.Diagnostics;
using AdvancedPath;
using MetricHunter.Application.Csv;
using MetricHunter.Application.Git;
using MetricHunter.Application.Metrics;
using MetricHunter.Application.Repositories;
using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Octokit;

namespace MetricHunter.Desktop.Presenters;

public class ViewMainPresenter : IViewMainPresenter
{
    private readonly IApplicationController _controller;
    private readonly ICsvHelper _csvHelper;
    private readonly IGitManager _gitManager;
    private readonly IGitProvider _gitProvider;
    private readonly ILogger<ViewMainPresenter> _logger;
    private readonly IMetricCalculatorManager _metricCalculatorManager;
    private readonly IRepositoryAppService _repositoryAppService;
    private List<Repository> _repositories;
    private List<Branch> _branches;

    public ViewMainPresenter(IViewMain view, IApplicationController controller)
    {
        View = view;
        _controller = controller;
        View.Presenter = this;

        _repositories = new List<Repository>();
        _branches = new List<Branch>();

        _gitManager = _controller.ServiceProvider.GetRequiredService<IGitManager>();
        _gitProvider = _controller.ServiceProvider.GetRequiredService<IGitProvider>();
        _metricCalculatorManager = _controller.ServiceProvider.GetRequiredService<IMetricCalculatorManager>();
        _csvHelper = _controller.ServiceProvider.GetRequiredService<ICsvHelper>();
        _repositoryAppService = _controller.ServiceProvider.GetRequiredService<IRepositoryAppService>();
        _logger = _controller.ServiceProvider.GetRequiredService<ILogger<ViewMainPresenter>>();

        Authenticate();
        
        LoadFromArgsAsync();
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
    
    public async Task<string> CalculateMetricsAsync(CancellationToken cancellationToken = default)
    {
        var repositoryList =
            await _repositoryAppService.ReadRepositoriesAsync(View.CalculateMetricsRepositoryPath, cancellationToken);
        if (_repositories.Any())
            repositoryList = repositoryList.Where(x => Repositories.Any(x2 => x2.Id == x.Id)).ToArray();

        var metrics = new List<Dictionary<string, string>>();
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        View.SetProgressBar(0);
        foreach (var item in repositoryList)
        {
            var language = GitConsts.LanguagesMap[item.Language];
            var manager = _metricCalculatorManager.FindMetricCalculator(language);
            var metric = await manager.CalculateMetricsAsync(item, item.DefaultBranch,
                View.CalculateMetricsRepositoryPath.ToFilePathString().ParentDirectory,
                View.CalculateMetricsByLocalResultsPath,
                cancellationToken);
            if (metric.IsEmpty())
                continue;
            var dictList = metric.ToDictionaryListByTopics();
            metrics.AddRange(dictList);
            View.SetProgressBar((int)((double)metrics.Count / repositoryList.Length * 100));
        }

        View.SetProgressBar(100);
        stopwatch.Stop();
        _logger.LogInformation($"Metrics calculated in {stopwatch.Elapsed:hh\\:mm\\:ss}");
        return _csvHelper.MetricsToCsv(metrics);
    }

    public async Task DownloadRepositoriesAsync(CancellationToken cancellationToken = default)
    {
        if (!CheckSelectRepositories())
            return;

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        View.SetProgressBar(0);
        var count = Repositories.Count();
        var i = 0;
        foreach (var item in Repositories)
        {
            await _gitProvider.CloneRepositoryAsync(item, View.DownloadRepositoryPath, item.DefaultBranch,
                cancellationToken);
            View.SetProgressBar((int)((double)++i / count * 100));
        }

        View.SetProgressBar(100);
        stopwatch.Stop();
        _logger.LogInformation($"Repositories downloaded in {stopwatch.Elapsed:hh\\:mm\\:ss}");
    }

    public async Task ShowRepositoriesAsync(CancellationToken cancellationToken = default)
    {
        if (View.JsonLoadPath.IsNullOrEmpty())
        {
            _controller.ErrorMessage("No file selected");
            return;
        }

        var repositories = await _repositoryAppService.ReadRepositoriesAsync(View.JsonLoadPath, cancellationToken);

        if (!repositories.Any()) return;

        Repositories = repositories;

        View.ShowRepositories(Repositories);
    }

    public async Task SaveRepositoriesAsync(CancellationToken cancellationToken = default)
    {
        if (!Repositories.Any())
        {
            _controller.ErrorMessage("No repositories found for save.");
            return;
        }

        await _repositoryAppService.WriteRepositoriesAsync(Repositories, View.JsonSavePath, cancellationToken);
    }

    public async Task<string> HuntRepositoriesAsync(CancellationToken cancellationToken = default)
    {
        if (!CheckSelectRepositories())
            return string.Empty;

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        View.SetProgressBar(0);
        var metrics = new List<Dictionary<string, string>>();
        foreach (var item in Repositories)
            if (await _gitProvider.CloneRepositoryAsync(item, cancellationToken: cancellationToken))
            {
                var language = GitConsts.LanguagesMap[item.Language];
                var manager = _metricCalculatorManager.FindMetricCalculator(language);
                var metric = await manager.CalculateMetricsAsync(item, token: cancellationToken);
                var dictList = metric.ToDictionaryListByTopics();
                metrics.AddRange(dictList);
                await _gitProvider.DeleteLocalRepositoryAsync(item, token: cancellationToken);
                View.SetProgressBar((int)((double)_repositories.IndexOf(item) / _repositories.Count * 100));
            }

        View.SetProgressBar(100);
        stopwatch.Stop();
        _logger.LogInformation($"Repositories hunted in {stopwatch.Elapsed:hh\\:mm\\:ss}");
        return _csvHelper.MetricsToCsv(metrics);
    }

    private async void LoadFromArgsAsync(CancellationToken cancellationToken = default)
    {
        var args = Environment.GetCommandLineArgs();
        if (args.Length > 1)
        {
            var files = args.Skip(1).Where(x => x.EndsWith(GitConsts.RepositoryInfoFileExtension)).ToArray();
            Console.WriteLine($"Loading {files.Length} files");
            var repositories = new List<Repository>();
            foreach (var file in files)
            {
                var repository = await _repositoryAppService.ReadRepositoriesAsync(file, cancellationToken);
                repositories.AddRange(repository);
            }

            Console.WriteLine($"Loaded {repositories.Count} repositories");
            Repositories = repositories;
        }
    }
    
    public void ShowGithubLogin()
    {
        _controller.ShowGithubLogin();
        Authenticate();
    }

    public void ShowExploreRepositories()
    {
        _controller.ShowExploreRepositories();
    }

    public void ShowFindRepository()
    {
        _controller.ShowFindRepository();
    }

    private void Authenticate()
    {
        if (!string.IsNullOrWhiteSpace(View.GithubToken))
            _gitManager.Authenticate(View.GithubToken);
    }

    private bool CheckSelectRepositories()
    {
        if (Repositories.Any()) return true;
        _controller.ErrorMessage("No repositories selected");
        return false;

    }
}