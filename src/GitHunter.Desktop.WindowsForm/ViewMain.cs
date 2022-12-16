using System.ComponentModel;
using System.Data;
using GitHunter.Application.Git;
using GitHunter.Application.LanguageStatistics;
using GitHunter.Desktop.Presenters;
using GitHunter.Desktop.Views;
using Octokit;
using Volo.Abp.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GitHunter.Desktop;

public partial class ViewMain : Form, ISingletonDependency, IViewMain
{
    public IViewMainPresenter Presenter { get; set; }
    
    public Language? SelectedLanguage => _languageComboBox.SelectedValue as Language?;
    
    public SortDirection SortDirection => _sortDirectionComboBox.SelectedValue as SortDirection? ?? SortDirection.Descending;
    
    public int RepositoryCount => int.TryParse(_repositoryCountTextBox.Text, out var repositoryCount) ? repositoryCount : 10;
    public string Topics => _topicsTextBox.Text;
    
    public IEnumerable<Language>? LanguageSelectList
    {
        set => _languageComboBox.DataSource = value;
    }

    public IEnumerable<SortDirection> SortDirectionSelectList
    {
        set => _sortDirectionComboBox.DataSource = value;
    }

    public void ShowRepositories(IEnumerable<Repository> repositories)
    {
        var bindingList = new BindingList<Repository>(repositories.ToList());
        _repositoryDataGridView.DataSource = new BindingSource(bindingList, null);
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

    private void _viewMain_Load(object sender, EventArgs e)
    {
        Presenter.LoadForm();
    }

}