using GitHunter.Application.Git;
using GitHunter.Application.LanguageStatistics;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public partial class MainForm : Form, ISingletonDependency
{
    private readonly IGitManager _githubManager;
    private readonly ILanguageStatisticsFactory _languageStatisticsFactory;

    public MainForm(IGitManager githubManager, ILanguageStatisticsFactory languageStatisticsFactory)
    {
        _githubManager = githubManager;
        _languageStatisticsFactory = languageStatisticsFactory;
        Load += MainForm_Load;

        InitializeComponent();
    }

    private async void MainForm_Load(object? sender, EventArgs e)
    {
        var results = await _githubManager.GetRepositories(new GitInput()
        {
            Count = 1,
            Language = Language.CSharp,
            Order = SortDirection.Ascending
        }, () =>
        {
            MessageBox.Show("Rate limit exceeded!");
        });

        await _githubManager.CloneRepository(results.Repositories[0]);
        var languageStatistics = _languageStatisticsFactory.GetLanguageStatistics(Language.CSharp);
        await languageStatistics.GetStatisticsAsync(results.Repositories[0]);
    }
}