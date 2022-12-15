using GitHunter.Application.Git;
using GitHunter.Application.LanguageStatistics;
using GitHunter.Desktop.Presenters;
using GitHunter.Desktop.Views;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public partial class ViewMain : Form, ISingletonDependency, IViewMain
{
    public IViewMainPresenter Presenter { get; set; }

    public Language? SelectedLanguage => _languageComboBox.SelectedValue as Language?;
    public SortDirection SortDirection => _sortDirectionComboBox.SelectedValue as SortDirection? ?? SortDirection.Descending;
    public int RepositoryCount => int.TryParse(_repositoryCountTextBox.Text, out var repositoryCount) ? repositoryCount : 10;
    public void ShowRepositories(IEnumerable<Repository> repositories)
    {
        _repositoryDataGridView.DataSource = repositories;
    }

    public ViewMain()
    {
        InitializeComponent();
    }
    
    public void Run()
    {
        System.Windows.Forms.Application.Run(this);
    }
    
    private void _calculateMetricButton_Click(object sender, EventArgs e)
    {

    }

    private void _downloadButton_Click(object sender, EventArgs e)
    {

    }

    private void _searchButton_Click(object sender, EventArgs e)
    {
        Presenter.SearchRepositories();
    }

    private void _jsonPathSelectButton_Click(object sender, EventArgs e)
    {

    }
}