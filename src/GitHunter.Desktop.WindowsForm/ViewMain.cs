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

  public Language? SelectedLanguage => _languageComboBox.SelectedValue as Language?;

  public SortDirection SortDirection =>
    _sortDirectionComboBox.SelectedValue as SortDirection? ?? SortDirection.Descending;

  public int RepositoryCount =>
    int.TryParse(_repositoryCountTextBox.Text, out var repositoryCount) ? repositoryCount : 10;

  public string Topics => _topicsTextBox.Text;
  public string RepositoriesJsonPath { get; set; }
  public string RepositoriesFolderPath { get; set; }

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
  }

  private void _downloadMetricsButton_Click(object sender, EventArgs e)
  {
    Presenter.DownloadMetrics();
  }

  private void showToolStripMenuItem_Click(object sender, EventArgs e)
  {
    using var fileDialog = new OpenFileDialog
    {
      Filter = "Json files | *.json"
    };

    if (fileDialog.ShowDialog() == DialogResult.OK)
    {
      RepositoriesJsonPath = fileDialog.FileName;
    }

    Presenter.ShowRepositories();
  }

  private void saveToolStripMenuItem_Click(object sender, EventArgs e)
  {
    using var folderDialog = new FolderBrowserDialog();

    if (folderDialog.ShowDialog() == DialogResult.OK)
    {
      RepositoriesFolderPath = folderDialog.SelectedPath;
    }

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
}
