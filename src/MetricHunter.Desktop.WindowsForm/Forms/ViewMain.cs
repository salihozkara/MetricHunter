using System.Diagnostics;
using System.Text;
using MetricHunter.Application.Git;
using MetricHunter.Application.Repositories;
using MetricHunter.Core.Paths;
using MetricHunter.Core.Processes;
using MetricHunter.Core.Tasks;
using MetricHunter.Desktop.Core;
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

namespace MetricHunter.Desktop.Forms;

public partial class ViewMain : Form, ISingletonDependency, IViewMain {
    private readonly ILogger<ViewMain> _logger;
    private readonly IProcessManager _processManager;

    public ViewMain(ILogger<ViewMain> logger, IProcessManager processManager) {
        _logger = logger;
        _processManager = processManager;
        InitializeComponent();
    }

    public IViewMainPresenter Presenter { get; set; }

    public CancellationTokenSource CancellationTokenSource { get; set; } = new();

    public void ShowMessage(string message) {
        MessageBox.Show(message);
    }

    public void SetProgressBar(int value) {
        value = value > 100 ? 100 : value;
        value = value < 0 ? 0 : value;
        _progressBar.Value = value;
        
        if (value == 100) {
            _progressBar.Value = 0;
            ButtonEnable();
        }else {
            ButtonDisable();
        }
    }

    public void ShowCommits(IEnumerable<GitHubCommit> gitHubCommits) {
        AddSelectColumn();

        var index = 0;

        var commitModelList = gitHubCommits.Select(x => new CommitModel() {
            Id = x.Sha,
            Index = ++index,
            Author = x.Commit.Author.Email,
            CommitedAt = x.Commit.Author.Date.UtcDateTime,
            Name = x.Commit.Message,
            Url = x.HtmlUrl
        }).ToList();

        _repositoryDataGridView.DataSource = commitModelList.ToList();

        // if (_repositoryDataGridView.Columns["Id"] != null) _repositoryDataGridView.Columns["Id"]!.Visible = false;
    }

    public void ShowReleases(IEnumerable<Release> releases) {
        AddSelectColumn();

        var index = 0;

        var releaseModelList = releases.Select(x => new ReleaseModel() {
            Id = x.TagName,
            Index = ++index,
            Name = x.Name,
            Url = x.HtmlUrl,
            PublishedAt = x.PublishedAt?.UtcDateTime,
        }).ToList();

        _repositoryDataGridView.DataSource = releaseModelList.ToList();

        if (_repositoryDataGridView.Columns["Id"] != null) _repositoryDataGridView.Columns["Id"]!.Visible = false;
    }

    public void CompleteRepository(string id)
    {
        var row = _repositoryDataGridView.Rows.Cast<DataGridViewRow>()
            .FirstOrDefault(r => r.Cells["Id"].Value.ToString() == id);

        if (row != null)
        {
            row.Cells[_completeCheckBoxColumnIndex].Value = true;
        }
    }

    public string GithubToken {
        get {
            try {
                return Settings.Default.GithubToken;
            }
            catch (Exception e) {
                return "";
            }
        }
    }

    private int _checkBoxColumnIndex = 0;
    private int _exploreButtonColumnIndex = -1;
    private int _completeCheckBoxColumnIndex = 0;

    public IEnumerable<string> SelectedRepositories {
        get {
            return _repositoryDataGridView.Rows.Cast<DataGridViewRow>()
                .Where(r => r.Cells[_checkBoxColumnIndex].Value != null && (bool)r.Cells[_checkBoxColumnIndex].Value)
                .Select(r => r.Cells["Id"].Value.ToString()!)
                .ToList();
        }
    }

    public string JsonLoadPath { get; set; }

    public string JsonSavePath { get; set; }

    public string DownloadRepositoryPath { get; set; }

    public string CalculateMetricsRepositoryPath { get; set; }

    public string CalculateMetricsByLocalResultsPath { get; set; }

    private void AddSelectColumn() {
        _repositoryDataGridView.Columns.Clear();

        _checkBoxColumnIndex = _repositoryDataGridView.Columns.Add(new DataGridViewCheckBoxColumn() {
            Name = "IsSelected",
            HeaderText = "Select All",
            DataPropertyName = "IsSelected",
            ValueType = typeof(bool),
            ReadOnly = true,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
            MinimumWidth = 150
        });
    }
    
    private void AddExploreColumn()
    {
        var exploreColumn = new DataGridViewImageColumn()
        {
            Name = "Explore",
            HeaderText = "Explore",
            DataPropertyName = "Explore",
            ReadOnly = true,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        };
        _exploreButtonColumnIndex = _repositoryDataGridView.Columns.Add(exploreColumn);
    }

    private void AddCompleteColumn()
    {
        var completeColumn = new DataGridViewCheckBoxColumn()
        {
            Name = "Complete",
            HeaderText = "Complete",
            DataPropertyName = "Complete",
            ValueType = typeof(bool),
            ReadOnly = true,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        };
        
        _completeCheckBoxColumnIndex = _repositoryDataGridView.Columns.Add(completeColumn);
    }

    public void ShowRepositories(IEnumerable<RepositoryWithBranchNameDto> repositories) {
        AddSelectColumn();

        var index = 0;
        var repositoryModelList = new List<dynamic>();
        var repositoryModelCount = 0;
        var commitModelCount = 0;
        var releaseModelCount = 0;
        var exploreButtonEnabled = false;

        foreach (var repositoryWithBranchNameDto in repositories) {
            switch (repositoryWithBranchNameDto.OtherData) {
                case null:
                    repositoryModelList.Add(new RepositoryModel {
                        Id = repositoryWithBranchNameDto.Repository.Id,
                        Index = ++index,
                        Stars = repositoryWithBranchNameDto.Repository.StargazersCount,
                        Name = repositoryWithBranchNameDto.Repository.Name,
                        Url = repositoryWithBranchNameDto.Repository.HtmlUrl,
                        Description = repositoryWithBranchNameDto.Repository.Description,
                        License = repositoryWithBranchNameDto.Repository.License?.Name ?? "No License",
                        Owner = repositoryWithBranchNameDto.Repository.Owner.Login,
                        Size = ToSizeString(repositoryWithBranchNameDto.Repository.Size)
                    });
                    repositoryModelCount++;
                    exploreButtonEnabled = true;
                    break;
                case GitHubCommit commit:
                    repositoryModelList.Add(new CommitModel {
                        Id = commit.Sha,
                        Index = ++index,
                        Author = commit.Commit.Author.Email,
                        CommitedAt = commit.Commit.Author.Date.UtcDateTime,
                        Name = commit.Commit.Message,
                        Url = commit.HtmlUrl
                    });
                    commitModelCount++;
                    break;
                case Release release:
                    repositoryModelList.Add(new ReleaseModel {
                        Id = release.TagName,
                        Index = ++index,
                        Name = release.Name,
                        Url = release.HtmlUrl,
                        PublishedAt = release.PublishedAt?.UtcDateTime,
                    });
                    releaseModelCount++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // _repositoryDataGridView.DataSource = repositoryModelList.ToList();
        // _repositoryDataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        // _repositoryDataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        // _repositoryDataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        // _repositoryDataGridView.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        // _repositoryDataGridView.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

        if (new[] { commitModelCount, repositoryModelCount, releaseModelCount }.Count(x => x > 0) > 1)
        {
            _repositoryDataGridView.DataSource = repositoryModelList.Select(x => new CommandModel
            {
                Id = x.Id.ToString(),
                Index = x.Index,
                Name = x.Name,
                Url = x.Url
            }).ToList();
        }
        else
        {
            _repositoryDataGridView.DataSource = repositoryModelList;
        }

        if (exploreButtonEnabled)
        {
            AddExploreColumn();
        }

        // if (_repositoryDataGridView.Columns["Id"] != null) _repositoryDataGridView.Columns["Id"]!.Visible = false;

        SetHyperLink();
        AddCompleteColumn();
    }

    public void Run() {
        System.Windows.Forms.Application.Run(this);
    }

    private static string ToSizeString(long size) // size in kilobytes
    {
        return size switch {
            < 1024 => $"{size} KB",
            < 1024 * 1024 => $"{Math.Round(size / 1024.0, 2)} MB",
            _ => $"{Math.Round(size / 1024.0 / 1024.0, 2)} GB"
        };
    }

    private void SetHyperLink() {
        if (_repositoryDataGridView.Columns.Contains("Url"))
            _repositoryDataGridView.Columns["Url"]!.DefaultCellStyle = new DataGridViewCellStyle {
                ForeColor = Color.Blue
            };
    }

    private void _viewMain_Load(object sender, EventArgs e) {
        DesktopSink.LogAction += (s, logEvent) => {
            if (logEvent.Level != LogEventLevel.Information) return;
            logTextBox.BeginInvoke(() =>
            {
                logTextBox.Text += s;
                LogScrollToBottom();
            });
        };

        _logger.LogInformation("Application initialized");

        LogScrollToBottom();
    }

    private void LogScrollToBottom() {
        logTextBox.SelectionStart = logTextBox.Text.Length;
        logTextBox.ScrollToCaret();
    }

    private void ButtonDisable() {
        _downloadButton.Enabled = false;
        _calculateMetricsButton.Enabled = false;
        _huntButton.Enabled = false;
        _cancelButton.Enabled = true;
        
        // all not complete
        foreach (DataGridViewRow row in _repositoryDataGridView.Rows)
        {
            var cell = row.Cells[_completeCheckBoxColumnIndex];
            
            if (cell is DataGridViewCheckBoxCell checkBoxCell)
            {
                checkBoxCell.Value = false;
            }
        }
    }

    private void ButtonEnable() {
        _downloadButton.Enabled = true;
        _calculateMetricsButton.Enabled = true;
        _huntButton.Enabled = true;
        _cancelButton.Enabled = false;
    }

    private string BuildFileDialogFilter(Dictionary<string, string> fileDialogFilter) {
        var filter = "";
        foreach (var (key, value) in fileDialogFilter) {
            if (filter != "")
                filter += "|";
            filter += $"{key}|*{value}";
        }

        return filter;
    }

    private async void _calculateMetricsButton_Click(object sender, EventArgs e) {
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

        if (fileDialog.ShowDialog() == DialogResult.OK) {
            CalculateMetricsRepositoryPath = fileDialog.FileName;
        }
        else {
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
        _progressBar.Value = 0;
    }

    private static async Task SaveCsv(string result) {
        var fileName = $"result_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.csv";

        using var fileDialog = new SaveFileDialog {
            Filter = "Csv files | *.csv",
            FileName = fileName
        };

        if (fileDialog.ShowDialog() == DialogResult.OK) await File.WriteAllTextAsync(fileDialog.FileName, result);
    }

    private async void _downloadButton_Click(object sender, EventArgs e) {
        _logger.LogInformation("Download operation started");
        _logger.LogInformation("Please wait...");
        ButtonDisable();
        CancellationTokenSource = new CancellationTokenSource();

        using var folderDialog = new FolderBrowserDialog();

        if (folderDialog.ShowDialog() == DialogResult.OK) DownloadRepositoryPath = folderDialog.SelectedPath;

        await Presenter.DownloadRepositoriesAsync(CancellationTokenSource.Token).MaybeCanceled(CancellationTokenSource.Token);

        ButtonEnable();
    }

    private void showToolStripMenuItem_Click(object sender, EventArgs e) {
        using var fileDialog = new OpenFileDialog {
            Filter = BuildFileDialogFilter(new Dictionary<string, string>
            {
                { "Metric Hunter Files", GitConsts.RepositoryInfoFileExtension },
                { "Json Files", ".json" }
            })
        };

        if (fileDialog.ShowDialog() == DialogResult.OK) JsonLoadPath = fileDialog.FileName;

        Presenter.ShowRepositoriesAsync();
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
        using var saveFileDialog = new SaveFileDialog {
            Filter = BuildFileDialogFilter(new Dictionary<string, string>
            {
                { "Metric Hunter Files", GitConsts.RepositoryInfoFileExtension },
                { "Json Files", ".json" }
            }),
            OverwritePrompt = false
        };

        if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

        JsonSavePath = saveFileDialog.FileName;
        Presenter.SaveRepositoriesAsync();
    }

    private void _repositoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {
        if (!_repositoryDataGridView.Columns[_repositoryDataGridView.CurrentCell.ColumnIndex].HeaderText
                .Contains("Url")) return;

        if (!string.IsNullOrWhiteSpace(_repositoryDataGridView.CurrentCell.EditedFormattedValue.ToString())) {
            var ps = new ProcessStartInfo(_repositoryDataGridView.CurrentCell.EditedFormattedValue.ToString()!) {
                UseShellExecute = true,
                Verb = "open"
            };

            Process.Start(ps);
        }
    }
    

    private void _repositoryDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
        switch (e) {
            case { RowIndex: -1 } when IsAllSelected && e.ColumnIndex == _checkBoxColumnIndex:
                UnSelectAllRepositories();
                _repositoryDataGridView.Columns[_checkBoxColumnIndex].HeaderCell.Value = "Select All";
                return;
            case { RowIndex: -1 } when !IsAllSelected && e.ColumnIndex == _checkBoxColumnIndex:
                SelectAllRepositories();
                _repositoryDataGridView.Columns[_checkBoxColumnIndex].HeaderCell.Value = "Unselect All";
                return;
            case { RowIndex: >= 0 } when e.ColumnIndex == _checkBoxColumnIndex : 
            {
                    if (_repositoryDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell cell) {
                        cell.Value = (bool?)cell.Value != true;
                        return;
                    }
                    break;
                }
            case { RowIndex: > -1 } when e.ColumnIndex == _exploreButtonColumnIndex:
            {
                var repository = _repositoryDataGridView.Rows[e.RowIndex].DataBoundItem as dynamic;
                Presenter.ExploreRepository(repository.Id.ToString());
                return;
            }
        }
        _repositoryDataGridView.Columns[_checkBoxColumnIndex].HeaderCell.Value = IsAllSelected ? "Unselect All" : "Select All";
    }

    private void SelectAllRepositories() {
        _repositoryDataGridView.Rows.Cast<DataGridViewRow>().ToList()
            .ForEach(row => row.Cells[_checkBoxColumnIndex].Value = true);
    }

    private void UnSelectAllRepositories(bool includeDataGridSelectedRows = true) {
        _repositoryDataGridView.Rows.Cast<DataGridViewRow>()
            .Where(row => includeDataGridSelectedRows || row.Selected == false).ToList()
            .ToList()
            .ForEach(row => row.Cells[_checkBoxColumnIndex].Value = false);
    }

    private bool IsAllSelected => _repositoryDataGridView.Rows.Cast<DataGridViewRow>().All(row => (bool?)row.Cells[_checkBoxColumnIndex].Value == true);

    private void loginGithubToolStripMenuItem_Click(object sender, EventArgs e) {
        Presenter.ShowGithubLogin();
    }

    private void contributorsToolStripMenuItem_Click(object sender, EventArgs e) {
        var contributorsLink = "https://github.com/salihozkara/MetricHunter/graphs/contributors";

        var ps = new ProcessStartInfo(contributorsLink) {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }

    private void reportToolStripMenuItem_Click(object sender, EventArgs e) {
        var reportLink = "https://github.com/salihozkara/MetricHunter/issues/new";

        var ps = new ProcessStartInfo(reportLink) {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }

    private void helpToolStripMenuItem_Click(object sender, EventArgs e) {
        var helpLink = "https://github.com/salihozkara/MetricHunter/blob/master/doc/UserGuide.md";

        var ps = new ProcessStartInfo(helpLink) {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }

    private async void huntButton_Click(object sender, EventArgs e) {
        _logger.LogInformation("Hunt operation started");
        _logger.LogInformation("This process may take some time. Please wait...");
        ButtonDisable();
        CancellationTokenSource = new CancellationTokenSource();

        var result = await Presenter.HuntRepositoriesAsync(CancellationTokenSource.Token)
            .MaybeCanceled(CancellationTokenSource.Token);
        ButtonEnable();
        if (string.IsNullOrEmpty(result))
            return;
        _logger.LogInformation("Hunt operation finished");
        await SaveCsv(result);
        _progressBar.Value = 0;
    }

    private void openLogsStripMenuItem_Click(object sender, EventArgs e) {
        var ps = new ProcessStartInfo(DesktopLogsConsts.LogFilePath.ParentDirectory) {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }

    private void cancelButton_Click(object sender, EventArgs e) {
        CancellationTokenSource.Cancel();
        CancellationTokenSource.Dispose();
        CancellationTokenSource = new CancellationTokenSource();
        _processManager.KillAllProcesses(true);
        _logger.LogInformation("Operation canceled by user");
        _progressBar.Value = 0;
        ButtonEnable();
    }

    private void findRepositoryToolStripMenuItem_Click(object sender, EventArgs e) {
        Presenter.ShowFindRepository();
    }

    private void exploreRepositoriesToolStripMenuItem_Click(object sender, EventArgs e) {
        Presenter.ShowExploreRepositories();
    }

    private void _repositoryDataGridView_DataContextChanged(object sender, EventArgs e) {
        SetHyperLink();

    }

    private void _repositoryDataGridView_SelectionChanged(object sender, EventArgs e) {
        var selectedRows = _repositoryDataGridView.SelectedRows;
        if (selectedRows.Count == 0) return;

        UnSelectAllRepositories();

        foreach (DataGridViewRow selectedRow in selectedRows) {
            if (selectedRow.Cells[_checkBoxColumnIndex] is not DataGridViewCheckBoxCell cell) return;
            cell.Value = true;
        }

        _repositoryDataGridView.Columns[_checkBoxColumnIndex].HeaderCell.Value = IsAllSelected ? "Unselect All" : "Select All";
    }
}