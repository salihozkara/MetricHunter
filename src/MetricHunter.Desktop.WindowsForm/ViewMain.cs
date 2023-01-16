using System.Diagnostics;
using MetricHunter.Application.Git;
using MetricHunter.Core.Paths;
using MetricHunter.Core.Tasks;
using MetricHunter.Desktop.DesktopLogs;
using MetricHunter.Desktop.Models;
using MetricHunter.Desktop.Presenters;
using MetricHunter.Desktop.Properties;
using MetricHunter.Desktop.Views;
using Octokit;
using Serilog.Events;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Desktop;

public partial class ViewMain : Form, ISingletonDependency, IViewMain
{
    public ViewMain()
    {
        InitializeComponent();
        DesktopSink.LogAction += (s, e) =>
        {
            if (e.Level == LogEventLevel.Error)
            {
                MessageBox.Show(s, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
    }

    public IViewMainPresenter Presenter { get; set; }

    public CancellationTokenSource SearchRepositoriesCancellationTokenSource { get; set; }
    public CancellationTokenSource DownloadRepositoriesCancellationTokenSource { get; set; }
    public CancellationTokenSource CalculateMetricsCancellationTokenSource { get; set; }
    public CancellationTokenSource HuntRepositoriesCancellationTokenSource { get; set; }

    public void ShowMessage(string message)
    {
        MessageBox.Show(message);
    }

    public void SetSearchProgressBar(int value)
    {
        value = value > 100 ? 100 : value;
        value = value < 0 ? 0 : value;
        _searchProgressBar.Value = value;
    }

    public string GithubToken
    {
        get
        {
            try
            {
                return Settings.Default.GithubToken;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

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

    public string CalculateMetricsRepositoryPath { get; set; }

    public string CalculateMetricsByLocalResultsPath { get; set; }

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
        var index = 0;
        var repositoryModelList = repositories.Select(x => new RepositoryModel
        {
            Id = x.Id,
            Index = ++index,
            Name = x.Name,
            Description = x.Description,
            Stars = x.StargazersCount,
            Url = x.HtmlUrl,
            License = x.License?.Name ?? "No License",
            Owner = x.Owner.Login,
            Size = ToSizeString(x.Size)
        }).ToList();
        _repositoryDataGridView.DataSource = repositoryModelList.ToList();

        if (_repositoryDataGridView.Columns["Id"] != null) _repositoryDataGridView.Columns["Id"]!.Visible = false;

        SetHyperLink();
    }

    public void Run()
    {
        System.Windows.Forms.Application.Run(this);
    }

    private static string ToSizeString(long size) // size in kilobytes
    {
        return size switch
        {
            < 1024 => $"{size} KB",
            < 1024 * 1024 => $"{Math.Round(size / 1024.0, 2)} MB",
            _ => $"{Math.Round(size / 1024.0 / 1024.0, 2)} GB"
        };
    }

    private void SetHyperLink()
    {
        if (_repositoryDataGridView.Columns.Contains("Url"))
            _repositoryDataGridView.Columns["Url"]!.DefaultCellStyle = new DataGridViewCellStyle
            {
                ForeColor = Color.Blue
            };
    }

    private void _viewMain_Load(object sender, EventArgs e)
    {
        Presenter.LoadForm();
    }

    private async void _searchButton_Click(object sender, EventArgs e)
    {
        SearchRepositoriesCancellationTokenSource = new CancellationTokenSource();
        SetSearchProgressBar(0);
        ButtonDisable(sender);
        await Presenter.SearchRepositories().MaybeCanceled(SearchRepositoriesCancellationTokenSource.Token);
        ButtonEnable(sender);
    }

    private static void ButtonDisable(object sender)
    {
        var button = sender as Button;
        button!.Enabled = false;
    }
    
    private static void ButtonEnable(object sender)
    {
        var button = sender as Button;
        button!.Enabled = true;
    }

    private string BuildFileDialogFilter(Dictionary<string, string> fileDialogFilter)
    {
        var filter = "";
        foreach (var (key, value) in fileDialogFilter)
        {
            if (filter != "")
                filter += "|";
            filter += $"{key}|*{value}";
        }

        return filter;
    }

    private async void _calculateMetricsButton_Click(object sender, EventArgs e)
    {
        ButtonDisable(sender);
        CalculateMetricsCancellationTokenSource = new CancellationTokenSource();
        using var fileDialog = new OpenFileDialog();
        fileDialog.Multiselect = false;
        fileDialog.Filter = BuildFileDialogFilter(new Dictionary<string, string>
        {
            { "Metric Hunter Files", GitConsts.RepositoryInfoFileExtension },
            { "Json Files", ".json" }
        });
        
        if(string.IsNullOrWhiteSpace(DownloadRepositoryPath))
            fileDialog.InitialDirectory = PathHelper.TempPath;

        if (fileDialog.ShowDialog() == DialogResult.OK)
            CalculateMetricsRepositoryPath = fileDialog.FileName;
        else
        {
            ButtonEnable(sender);
            return;
        }

        using var folderDialog = new FolderBrowserDialog();

        folderDialog.Description = "Select the local results folder";

        if (folderDialog.ShowDialog() == DialogResult.OK)
            CalculateMetricsByLocalResultsPath = folderDialog.SelectedPath;

        var result = await Presenter.CalculateMetrics().MaybeCanceled(CalculateMetricsCancellationTokenSource.Token);
        ButtonEnable(sender);
        if(string.IsNullOrEmpty(result))
            return;
        await SaveCsv(result);
    }

    private static async Task SaveCsv(string result)
    {
        using var fileDialog = new SaveFileDialog
        {
            Filter = "Csv files | *.csv"
        };

        if (fileDialog.ShowDialog() == DialogResult.OK) await File.WriteAllTextAsync(fileDialog.FileName, result);
    }

    private async void _downloadButton_Click(object sender, EventArgs e)
    {
        ButtonDisable(sender);
        DownloadRepositoriesCancellationTokenSource = new CancellationTokenSource();
        
        using var folderDialog = new FolderBrowserDialog();

        if (folderDialog.ShowDialog() == DialogResult.OK) DownloadRepositoryPath = folderDialog.SelectedPath;
        
        await Presenter.DownloadRepositories().MaybeCanceled(DownloadRepositoriesCancellationTokenSource.Token);
        ButtonEnable(sender);
    }

    private void showToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var fileDialog = new OpenFileDialog
        {
            Filter = "Json files | *.json"
        };

        if (fileDialog.ShowDialog() == DialogResult.OK) JsonLoadPath = fileDialog.FileName;

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

        if (!string.IsNullOrWhiteSpace(_repositoryDataGridView.CurrentCell.EditedFormattedValue.ToString()))
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
        var contributorsLink = "https://github.com/salihozkara/MetricHunter/graphs/contributors";

        var ps = new ProcessStartInfo(contributorsLink)
        {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }

    private void reportToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var reportLink = "https://github.com/salihozkara/MetricHunter/issues/new";

        var ps = new ProcessStartInfo(reportLink)
        {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }

    private void helpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var helpLink = "https://github.com/salihozkara/MetricHunter/blob/master/doc/UserGuide.md";

        var ps = new ProcessStartInfo(helpLink)
        {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }

    private async void huntButton_Click(object sender, EventArgs e)
    {
        ButtonDisable(sender);
        HuntRepositoriesCancellationTokenSource = new CancellationTokenSource();
        var result = await Presenter.HuntRepositories().MaybeCanceled(HuntRepositoriesCancellationTokenSource.Token);
        ButtonEnable(sender);
        if (string.IsNullOrEmpty(result))
            return;
        await SaveCsv(result);
    }
}