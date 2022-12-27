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
    private IEnumerable<Repository> Repositories;

    public ViewMainPresenter(IViewMain view, IApplicationController controller)
    {
        View = view;
        _controller = controller;
        View.Presenter = this;

        _gitManager = _controller.ServiceProvider.GetRequiredService<IGitManager>();
        _gitProvider = _controller.ServiceProvider.GetRequiredService<IGitProvider>();
        _metricCalculatorManager = _controller.ServiceProvider.GetRequiredService<IMetricCalculatorManager>();
        _csvHelper = _controller.ServiceProvider.GetRequiredService<ICsvHelper>();
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

        Repositories = gitResult.Repositories;

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
            throw new UserFriendlyException("Önce repoları arayın.");

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
            throw new UserFriendlyException("Önce repoları arayın.");

        if (View.SelectedRepositories.Any())
        {
            var selectedRepos = Repositories.Where(x => View.SelectedRepositories.Contains(x.Id)).ToList();

            foreach (var item in selectedRepos) await _gitProvider.CloneRepository(item);
            return;
        }

        foreach (var item in Repositories) await _gitProvider.CloneRepository(item);
    }

    public void ShowRepositories()
    {
        if (View.RepositoriesJsonPath.IsNullOrEmpty())
        {
          throw new UserFriendlyException("Repository bulunamadı");
        }

        Repositories = JsonConvert.DeserializeObject<Repository[]>(File.ReadAllText($"{View.RepositoriesJsonPath}"), Resource.Jsons.JsonSerializerSettings);

        if (Repositories != null && !Repositories.Any()) return;

        var repositoryModelList = Repositories!.Select(x => new RepositoryModel
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

    public Task SaveRepositories()
    {
        if (!Repositories.Any())
            throw new UserFriendlyException("Önce repoları arayın.");

        var json = JsonConvert.SerializeObject(Repositories, Resource.Jsons.JsonSerializerSettings);

        var currentDate = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
        var fileName = $"{View.RepositoriesFolderPath}//repositories_{currentDate}.json";

        // Save file with create moed
        using var fileStream = new FileStream(fileName , FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
        fileStream.WriteAsync(Encoding.UTF8.GetBytes(json));

        return Task.CompletedTask;
    }
}
