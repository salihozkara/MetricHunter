using System.ComponentModel;
using System.Diagnostics;
using GitHunter.Desktop.Models;
using GitHunter.Desktop.Presenters;
using GitHunter.Desktop.Views;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public partial class ViewMain : Form, ISingletonDependency, IViewMain
{
    public ViewMain()
    {
        InitializeComponent();
    }

    public IViewMainPresenter Presenter { get; set; }

    public void ShowMessage(string message)
    {
        MessageBox.Show(message);   
    }

    public string GithubToken => Properties.Settings.Default.GithubToken;

    public IEnumerable<long> SelectedRepositories
    {
        get
        {
            return _repositoryDataGridView.SelectedRows.Cast<DataGridViewRow>()
              .Select(r => r.Cells[0].Value)
              .Cast<long>()
              .ToList();
        }
    }

    public Language? SelectedLanguage => _languageComboBox.SelectedValue as Language?;

    public SortDirection SortDirection =>
      _sortDirectionComboBox.SelectedValue as SortDirection? ?? SortDirection.Descending;

    public int RepositoryCount =>
      int.TryParse(_repositoryCountTextBox.Text, out var repositoryCount) ? repositoryCount : 10;

    public string Topics => _topicsTextBox.Text;
    
    public string JsonLoadPath { get; set; }
    
    public string JsonSavePath { get; set; }
    
    public string DownloadRepositoryPath { get; set; }

    public IEnumerable<Language>? LanguageSelectList
    {
        set => _languageComboBox.DataSource = value;
    }

    public IEnumerable<SortDirection> SortDirectionSelectList
    {
        set => _sortDirectionComboBox.DataSource = value;
    }
    
    public void ShowRepositories(IEnumerable<RepositoryModel> repositories)
    {
        _repositoryDataGridView.DataSource = repositories.ToList();

        if (_repositoryDataGridView.Columns["Id"] != null)
        {
            _repositoryDataGridView.Columns["Id"]!.Visible = false;
        }

        SetHyperLink();
    }

    private void SetHyperLink()
    {
        if (_repositoryDataGridView.Columns.Contains("Url"))
        {
            _repositoryDataGridView.Columns["Url"]!.DefaultCellStyle = new DataGridViewCellStyle
            {
                ForeColor = Color.Blue,
            };
        }
    }

    public void Run()
    {
        System.Windows.Forms.Application.Run(this);
    }

    private void _viewMain_Load(object sender, EventArgs e)
    {
        Presenter.LoadForm();
    }

    private void _searchButton_Click(object sender, EventArgs e)
    {
        Presenter.SearchRepositories();
    }

    private async void _calculateMetricsButton_Click(object sender, EventArgs e)
    {
        var result = await Presenter.CalculateMetrics();
        using var fileDialog = new SaveFileDialog
        {
            Filter = "Csv files | *.csv"
        };

        if (fileDialog.ShowDialog() == DialogResult.OK)
        {
            await File.WriteAllTextAsync(fileDialog.FileName, result);
        }
    }

    private void _downloadButton_Click(object sender, EventArgs e)
    {
        // using var folderDialog = new FolderBrowserDialog();
        //
        // if (folderDialog.ShowDialog() != DialogResult.OK) return;
        //
        // DownloadRepositoryPath = folderDialog.SelectedPath;
        DownloadRepositoryPath = "";
        Presenter.DownloadRepositories();
    }

    private void showToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var fileDialog = new OpenFileDialog
        {
            Filter = "Json files | *.json"
        };

        if (fileDialog.ShowDialog() == DialogResult.OK)
        {
            JsonLoadPath = fileDialog.FileName;
        }

        Presenter.ShowRepositories();
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var saveFileDialog = new SaveFileDialog
        {
            Filter = "Json files | *.json",
            OverwritePrompt = false
        };

        if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
        
        JsonSavePath = saveFileDialog.FileName;
        Presenter.SaveRepositories();

    }

    private void _repositoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (!_repositoryDataGridView.Columns[_repositoryDataGridView.CurrentCell.ColumnIndex].HeaderText
              .Contains("Url")) return;

        if (!String.IsNullOrWhiteSpace(_repositoryDataGridView.CurrentCell.EditedFormattedValue.ToString()))
        {
            var ps = new ProcessStartInfo(_repositoryDataGridView.CurrentCell.EditedFormattedValue.ToString()!)
            {
                UseShellExecute = true,
                Verb = "open"
            };

            Process.Start(ps);
        }
    }

    private void loginGithubToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Presenter.ShowGithubLogin();
    }

    private void contributorsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var contributorsLink = "https://github.com/salihozkara/GitHunter/graphs/contributors";

        var ps = new ProcessStartInfo(contributorsLink)
        {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }

    private void reportToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var reportLink = "https://github.com/salihozkara/GitHunter/issues/new";

        var ps = new ProcessStartInfo(reportLink)
        {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }

    private void helpToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }
}
