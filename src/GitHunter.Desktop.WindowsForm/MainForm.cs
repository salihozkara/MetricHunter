using GitHunter.Application.Git;
using GitHunter.Application.LanguageStatistics;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public partial class MainForm : Form, ISingletonDependency
{
    private readonly IGitManager _githubManager;
    private readonly IMetricCalculatorManager _metricCalculatorManager;
    private GitOutput? result;

    public MainForm(IGitManager githubManager, IMetricCalculatorManager metricCalculatorManager)
    {
        _githubManager = githubManager;
        _metricCalculatorManager = metricCalculatorManager;
        Load += MainForm_Load;

        InitializeComponent();
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
        
        result = await _githubManager.GetRepositories(gitInput, () =>
        {
            MessageBox.Show("Rate limit exceeded!");
        });

        repositoryDataGrid.DataSource = result.Repositories;
    }

    private async void downloadButton_Click(object sender, EventArgs e)
    {
        if (result != null)
            foreach (var item in result.Repositories)
            {
                await _githubManager.CloneRepository(item);
            }
    }

    private async void calculateMetricButton_Click(object sender, EventArgs e)
    {
        var selectedLanguage = (languageComboBox.SelectedValue as Language?);

        if (selectedLanguage != null)
        {
            var manager = _metricCalculatorManager.FindMetricCalculator(selectedLanguage.Value);

            if (result != null)
                foreach (var item in result.Repositories)
                {
                    await manager.CalculateMetricsAsync(item);
                }
        }
    }
}