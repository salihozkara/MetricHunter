using GitHunter.Application.Git;
using GitHunter.Application.LanguageStatistics;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public partial class MainForm : Form, ISingletonDependency
{
    private readonly IGitManager _githubManager;
    private readonly IGitManager _githubManager2;
    private readonly IMetricCalculatorManager _metricCalculatorManager;
    private GitOutput? _result;

    public MainForm(IGitManager githubManager, IMetricCalculatorManager metricCalculatorManager, IGitManager githubManager2)
    {
        _githubManager = githubManager;
        _metricCalculatorManager = metricCalculatorManager;
        _githubManager2 = githubManager2;
        Load += MainForm_Load;
        _githubManager.SearchRepositoriesRequestError += GithubManager_SearchRepositoriesRequestError;
        _githubManager.SearchRepositoriesRequestSuccess += GithubManager_SearchRepositoriesRequestSuccess;
        InitializeComponent();
    }

    private void GithubManager_SearchRepositoriesRequestSuccess(object? sender, SearchRepositoriesRequestSuccessEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void GithubManager_SearchRepositoriesRequestError(object? sender, SearchRepositoriesRequestErrorEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        languageComboBox.DataSource = _metricCalculatorManager.GetSupportedLanguages();
        orderTypeComboBox.DataSource = Enum.GetValues<SortDirection>();
    }

    private async void searchButton_Click(object sender, EventArgs e)
    {
        var gitInput = new GitInput()
        {
            Language = languageComboBox.SelectedValue as Language?,
            Order = (orderTypeComboBox.SelectedValue as SortDirection?) ?? SortDirection.Descending
        };

        if (int.TryParse(repositoryCountTextBox.Text, out var repositoryCount))
        {
            gitInput.Count = repositoryCount;
        }
        
        _result = await _githubManager.GetRepositories(gitInput);

        repositoryDataGrid.DataSource = _result.Repositories;
    }

    private async void downloadButton_Click(object sender, EventArgs e)
    {
        if (_result == null) return;
        foreach (var item in _result.Repositories)
        {
            await _githubManager.CloneRepository(item);
        }
    }

    private async void calculateMetricButton_Click(object sender, EventArgs e)
    {
        if (languageComboBox.SelectedValue is not Language selectedLanguage) return;
        var manager = _metricCalculatorManager.FindMetricCalculator(selectedLanguage);

        if (_result == null) return;
        foreach (var item in _result.Repositories)
        {
            await manager.CalculateMetricsAsync(item);
        }
    }
}