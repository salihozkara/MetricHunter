using GitHunter.Application.Git;
using GitHunter.Application.LanguageStatistics;
using Newtonsoft.Json;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public partial class MainForm : Form, ISingletonDependency
{
    private readonly IGitManager _githubManager;
    private readonly IGitProvider _gitProvider;
    private readonly IMetricCalculatorManager _metricCalculatorManager;
    private readonly List<GitOutput> _results = new();
    private GitOutput? _result;

    public MainForm(IGitManager githubManager, IMetricCalculatorManager metricCalculatorManager,
        IGitProvider gitProvider)
    {
        _githubManager = githubManager;
        _metricCalculatorManager = metricCalculatorManager;
        _gitProvider = gitProvider;
        Load += MainForm_Load;
        _githubManager.SearchRepositoriesRequestError += GithubManager_SearchRepositoriesRequestError;
        _githubManager.SearchRepositoriesRequestSuccess += GithubManager_SearchRepositoriesRequestSuccess;
        _githubManager.SearchRepositoriesRequestFinished += GithubManager_SearchRepositoriesRequestFinished;
        InitializeComponent();
    }

    private void GithubManager_SearchRepositoriesRequestFinished(object? sender,
        SearchRepositoriesRequestFinishedEventArgs e)
    {
        if (e.FailedRequests.Any())
        {
            var result = MessageBox.Show("Rate limit exceeded. Please try again later or use a vpn.", "Error",
                MessageBoxButtons.CancelTryContinue, MessageBoxIcon.Error);
            if (result == DialogResult.TryAgain)
            {
                var thread = new Thread(async () =>
                {
                    var r = await _githubManager.RetryFailedRequest();
                    Invoke(() => { _results.Add(r); });
                });
                thread.Start();
            }
        }
        else
        {
            File.WriteAllText("result.json", JsonConvert.SerializeObject(_githubManager.GetAllSuccessRepositories));
            MessageBox.Show("Finished");
        }
    }

    private void GithubManager_SearchRepositoriesRequestSuccess(object? sender,
        SearchRepositoriesRequestSuccessEventArgs e)
    {
    }

    private void GithubManager_SearchRepositoriesRequestError(object? sender, SearchRepositoriesRequestErrorEventArgs e)
    {
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        languageComboBox.DataSource = _metricCalculatorManager.GetSupportedLanguages();
        orderTypeComboBox.DataSource = Enum.GetValues<SortDirection>();
    }

    private void searchButton_Click(object sender, EventArgs e)
    {
        var gitInput = new GitInput
        {
            Language = languageComboBox.SelectedValue as Language?,
            Order = orderTypeComboBox.SelectedValue as SortDirection? ?? SortDirection.Descending
        };

        _githubManager.Clear();
        if (int.TryParse(repositoryCountTextBox.Text, out var repositoryCount)) gitInput.Count = repositoryCount;
        var thread = new Thread(async () =>
        {
            _result = await _githubManager.GetRepositories(gitInput);
            Invoke(() =>
            {
                if (_result != null) repositoryDataGrid.DataSource = _result.Repositories;
            });
        });
        thread.Start();
    }

    private async void downloadButton_Click(object sender, EventArgs e)
    {
        if (_result == null) return;
        foreach (var item in _result.Repositories) await _gitProvider.CloneRepository(item);
    }

    private async void calculateMetricButton_Click(object sender, EventArgs e)
    {
        if (languageComboBox.SelectedValue is not Language selectedLanguage) return;
        var manager = _metricCalculatorManager.FindMetricCalculator(selectedLanguage);

        if (_result == null) return;
        foreach (var item in _result.Repositories) await manager.CalculateMetricsAsync(item);
    }
}