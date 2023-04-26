using System.Diagnostics;
using MetricHunter.Application.Git;
using MetricHunter.Core.Paths;
using MetricHunter.Core.Processes;
using MetricHunter.Core.Tasks;
using MetricHunter.Desktop.DesktopLogs;
using MetricHunter.Desktop.Models;
using MetricHunter.Desktop.Presenters;
using MetricHunter.Desktop.Properties;
using MetricHunter.Desktop.Views;
using Microsoft.Extensions.Logging;
using Octokit;
using Serilog.Events;
using Volo.Abp.DependencyInjection;
using Language = MetricHunter.Core.Languages.Language;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;

namespace MetricHunter.Desktop;

public partial class ViewMain : Form, ISingletonDependency, IViewMain
{
    private readonly ILogger<ViewMain> _logger;
    private readonly IProcessManager _processManager;

    public ViewMain(ILogger<ViewMain> logger, IProcessManager processManager)
    {
        _logger = logger;
        _processManager = processManager;
        InitializeComponent();
    }

    public IViewMainPresenter Presenter { get; set; }

    public CancellationTokenSource CancellationTokenSource { get; set; }

    public void ShowMessage(string message)
    {
        MessageBox.Show(message);
    }

    public void SetProgressBar(int value)
    {
        if (CancellationTokenSource.IsCancellationRequested)
            return;
        value = value > 100 ? 100 : value;
        value = value < 0 ? 0 : value;
        _progressBar.Value = value;
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
        LogScrollToBottom();

        DesktopSink.LogAction += (s, logEvent) =>
        {
            if (logEvent.Level != LogEventLevel.Information) return;
            logTextBox.Text += s;
            LogScrollToBottom();
        };
    }

    private void LogScrollToBottom()
    {
        logTextBox.SelectionStart = logTextBox.Text.Length;
        logTextBox.ScrollToCaret();
    }

    private async void _searchButton_Click(object sender, EventArgs e)
    {
        _logger.LogInformation("Search operation started");
        CancellationTokenSource = new CancellationTokenSource();
        SetProgressBar(0);
        ButtonDisable();
        await Presenter.SearchRepositoriesAsync(CancellationTokenSource.Token)
            .MaybeCanceled(CancellationTokenSource.Token);
        ButtonEnable();
        _logger.LogInformation("Search operation finished");
    }

    private void ButtonDisable()
    {
        _searchButton.Enabled = false;
        _downloadButton.Enabled = false;
        _calculateMetricsButton.Enabled = false;
        _huntButton.Enabled = false;
        _cancelButton.Enabled = true;
    }

    private void ButtonEnable()
    {
        _searchButton.Enabled = true;
        _downloadButton.Enabled = true;
        _calculateMetricsButton.Enabled = true;
        _huntButton.Enabled = true;
        _cancelButton.Enabled = false;
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
        _logger.LogInformation("Calculate metrics operation started");
        ButtonDisable();
        CancellationTokenSource = new CancellationTokenSource();
        using var fileDialog = new OpenFileDialog();
        fileDialog.Multiselect = false;
        fileDialog.Filter = BuildFileDialogFilter(new Dictionary<string, string>
        {
            { "Metric Hunter Files", GitConsts.RepositoryInfoFileExtension },
            { "Json Files", ".json" }
        });

        if (string.IsNullOrWhiteSpace(DownloadRepositoryPath))
            fileDialog.InitialDirectory = PathHelper.TempPath;

        if (fileDialog.ShowDialog() == DialogResult.OK)
        {
            CalculateMetricsRepositoryPath = fileDialog.FileName;
        }
        else
        {
            _logger.LogInformation("Calculate metrics operation canceled");
            ButtonEnable();
            return;
        }

        using var folderDialog = new FolderBrowserDialog();

        folderDialog.Description = "Select the local results folder";

        if (folderDialog.ShowDialog() == DialogResult.OK)
            CalculateMetricsByLocalResultsPath = folderDialog.SelectedPath;

        var result = await Presenter.CalculateMetricsAsync(CancellationTokenSource.Token)
            .MaybeCanceled(CancellationTokenSource.Token);
        ButtonEnable();
        if (string.IsNullOrEmpty(result))
            return;
        _logger.LogInformation("Calculate metrics operation finished");
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
        _logger.LogInformation("Download operation started");
        ButtonDisable();
        CancellationTokenSource = new CancellationTokenSource();

        using var folderDialog = new FolderBrowserDialog();

        if (folderDialog.ShowDialog() == DialogResult.OK) DownloadRepositoryPath = folderDialog.SelectedPath;

        await Presenter.DownloadRepositoriesAsync(CancellationTokenSource.Token)
            .MaybeCanceled(CancellationTokenSource.Token);
        ButtonEnable();
    }

    private void showToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var fileDialog = new OpenFileDialog
        {
            Filter = "Json files | *.json"
        };

        if (fileDialog.ShowDialog() == DialogResult.OK) JsonLoadPath = fileDialog.FileName;

        Presenter.ShowRepositoriesAsync();
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
        Presenter.SaveRepositoriesAsync();
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
        _logger.LogInformation("Hunt operation started");
        ButtonDisable();
        CancellationTokenSource = new CancellationTokenSource();
        var result = await Presenter.HuntRepositoriesAsync(CancellationTokenSource.Token)
            .MaybeCanceled(CancellationTokenSource.Token);
        ButtonEnable();
        if (string.IsNullOrEmpty(result))
            return;
        _logger.LogInformation("Hunt operation finished");
        await SaveCsv(result);
    }

    private void openLogsStripMenuItem_Click(object sender, EventArgs e)
    {
        var ps = new ProcessStartInfo(DesktopLogsConsts.LogFilePath.ParentDirectory)
        {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        CancellationTokenSource.Cancel();
        _processManager.KillAllProcesses(true);
        _logger.LogInformation("Operation canceled by user");
        ButtonEnable();
    }
}