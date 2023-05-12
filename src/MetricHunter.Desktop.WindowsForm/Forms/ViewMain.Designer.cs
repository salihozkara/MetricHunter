namespace MetricHunter.Desktop.Forms;

partial class ViewMain {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewMain));
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        SearchButton = new ImageList(components);
        IconList = new ImageList(components);
        _repositoryDataGridView = new DataGridView();
        menu = new MenuStrip();
        savedRepositoriesToolStripMenuItem = new ToolStripMenuItem();
        exploreRepositoriesToolStripMenuItem = new ToolStripMenuItem();
        findRepositoryToolStripMenuItem = new ToolStripMenuItem();
        showToolStripMenuItem = new ToolStripMenuItem();
        saveToolStripMenuItem = new ToolStripMenuItem();
        loginGithubToolStripMenuItem = new ToolStripMenuItem();
        helpMenu = new ToolStripMenuItem();
        contributorsToolStripMenuItem = new ToolStripMenuItem();
        helpToolStripMenuItem = new ToolStripMenuItem();
        reportToolStripMenuItem = new ToolStripMenuItem();
        openLogsStripMenuItem = new ToolStripMenuItem();
        basePanel = new Panel();
        layoutPanel = new TableLayoutPanel();
        searchRepositoryPanel = new Panel();
        label1 = new Label();
        operationGroupBox = new GroupBox();
        _huntButton = new Button();
        _downloadButton = new Button();
        _calculateMetricsButton = new Button();
        bottomPanel = new Panel();
        bottomLayoutPanel = new TableLayoutPanel();
        logTextBox = new RichTextBox();
        panel1 = new Panel();
        _cancelButton = new Button();
        _progressBar = new ProgressBar();
        ((System.ComponentModel.ISupportInitialize)_repositoryDataGridView).BeginInit();
        menu.SuspendLayout();
        basePanel.SuspendLayout();
        layoutPanel.SuspendLayout();
        searchRepositoryPanel.SuspendLayout();
        operationGroupBox.SuspendLayout();
        bottomPanel.SuspendLayout();
        bottomLayoutPanel.SuspendLayout();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // SearchButton
        // 
        SearchButton.ColorDepth = ColorDepth.Depth8Bit;
        SearchButton.ImageStream = (ImageListStreamer)resources.GetObject("SearchButton.ImageStream");
        SearchButton.TransparentColor = Color.Transparent;
        SearchButton.Images.SetKeyName(0, "search.png");
        // 
        // IconList
        // 
        IconList.ColorDepth = ColorDepth.Depth8Bit;
        IconList.ImageStream = (ImageListStreamer)resources.GetObject("IconList.ImageStream");
        IconList.TransparentColor = Color.Transparent;
        IconList.Images.SetKeyName(0, "search.png");
        IconList.Images.SetKeyName(1, "knife.png");
        IconList.Images.SetKeyName(2, "calculation.png");
        IconList.Images.SetKeyName(3, "download.png");
        IconList.Images.SetKeyName(4, "close.png");
        // 
        // _repositoryDataGridView
        // 
        _repositoryDataGridView.AllowUserToAddRows = false;
        _repositoryDataGridView.AllowUserToDeleteRows = false;
        _repositoryDataGridView.AllowUserToOrderColumns = true;
        _repositoryDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _repositoryDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        _repositoryDataGridView.BackgroundColor = Color.FromArgb(215, 211, 193);
        _repositoryDataGridView.BorderStyle = BorderStyle.Fixed3D;
        _repositoryDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(138, 129, 124);
        dataGridViewCellStyle1.Font = new Font("Dubai", 12F, FontStyle.Bold, GraphicsUnit.Point);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        _repositoryDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        _repositoryDataGridView.ColumnHeadersHeight = 70;
        _repositoryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(239, 237, 230);
        dataGridViewCellStyle2.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
        dataGridViewCellStyle2.ForeColor = Color.Black;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        _repositoryDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
        _repositoryDataGridView.Dock = DockStyle.Fill;
        _repositoryDataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
        _repositoryDataGridView.Location = new Point(5, 150);
        _repositoryDataGridView.Margin = new Padding(5, 0, 5, 18);
        _repositoryDataGridView.Name = "_repositoryDataGridView";
        _repositoryDataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(138, 129, 124);
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
        _repositoryDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
        _repositoryDataGridView.RowHeadersVisible = false;
        _repositoryDataGridView.RowHeadersWidth = 30;
        _repositoryDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        _repositoryDataGridView.RowTemplate.Height = 50;
        _repositoryDataGridView.Size = new Size(777, 291);
        _repositoryDataGridView.TabIndex = 4;
        _repositoryDataGridView.CellClick += _repositoryDataGridView_CellClick;
        _repositoryDataGridView.CellContentDoubleClick += _repositoryDataGridView_CellContentClick;
        _repositoryDataGridView.SelectionChanged += _repositoryDataGridView_SelectionChanged;
        _repositoryDataGridView.DataContextChanged += _repositoryDataGridView_DataContextChanged;
        // 
        // menu
        // 
        menu.BackColor = Color.FromArgb(215, 211, 193);
        menu.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        menu.ImageScalingSize = new Size(24, 24);
        menu.Items.AddRange(new ToolStripItem[] { savedRepositoriesToolStripMenuItem, loginGithubToolStripMenuItem, helpMenu });
        menu.Location = new Point(0, 0);
        menu.Name = "menu";
        menu.Padding = new Padding(4, 1, 0, 1);
        menu.Size = new Size(829, 30);
        menu.TabIndex = 15;
        menu.Text = "menuStrip1";
        // 
        // savedRepositoriesToolStripMenuItem
        // 
        savedRepositoriesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exploreRepositoriesToolStripMenuItem, findRepositoryToolStripMenuItem, showToolStripMenuItem, saveToolStripMenuItem });
        savedRepositoriesToolStripMenuItem.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
        savedRepositoriesToolStripMenuItem.Name = "savedRepositoriesToolStripMenuItem";
        savedRepositoriesToolStripMenuItem.Size = new Size(82, 28);
        savedRepositoriesToolStripMenuItem.Text = "Repository";
        // 
        // exploreRepositoriesToolStripMenuItem
        // 
        exploreRepositoriesToolStripMenuItem.Name = "exploreRepositoriesToolStripMenuItem";
        exploreRepositoriesToolStripMenuItem.Size = new Size(194, 28);
        exploreRepositoriesToolStripMenuItem.Text = "Explore Repositories";
        exploreRepositoriesToolStripMenuItem.Click += exploreRepositoriesToolStripMenuItem_Click;
        // 
        // findRepositoryToolStripMenuItem
        // 
        findRepositoryToolStripMenuItem.Name = "findRepositoryToolStripMenuItem";
        findRepositoryToolStripMenuItem.Size = new Size(194, 28);
        findRepositoryToolStripMenuItem.Text = "Find Repository";
        findRepositoryToolStripMenuItem.Click += findRepositoryToolStripMenuItem_Click;
        // 
        // showToolStripMenuItem
        // 
        showToolStripMenuItem.Name = "showToolStripMenuItem";
        showToolStripMenuItem.Size = new Size(194, 28);
        showToolStripMenuItem.Text = "Load";
        showToolStripMenuItem.Click += showToolStripMenuItem_Click;
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.Size = new Size(194, 28);
        saveToolStripMenuItem.Text = "Save";
        saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
        // 
        // loginGithubToolStripMenuItem
        // 
        loginGithubToolStripMenuItem.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
        loginGithubToolStripMenuItem.Name = "loginGithubToolStripMenuItem";
        loginGithubToolStripMenuItem.Size = new Size(97, 28);
        loginGithubToolStripMenuItem.Text = "Login GitHub";
        loginGithubToolStripMenuItem.Click += loginGithubToolStripMenuItem_Click;
        // 
        // helpMenu
        // 
        helpMenu.DropDownItems.AddRange(new ToolStripItem[] { contributorsToolStripMenuItem, helpToolStripMenuItem, reportToolStripMenuItem, openLogsStripMenuItem });
        helpMenu.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
        helpMenu.Name = "helpMenu";
        helpMenu.Size = new Size(48, 28);
        helpMenu.Text = "Help";
        // 
        // contributorsToolStripMenuItem
        // 
        contributorsToolStripMenuItem.Name = "contributorsToolStripMenuItem";
        contributorsToolStripMenuItem.Size = new Size(167, 28);
        contributorsToolStripMenuItem.Text = "Contributors";
        contributorsToolStripMenuItem.Click += contributorsToolStripMenuItem_Click;
        // 
        // helpToolStripMenuItem
        // 
        helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        helpToolStripMenuItem.Size = new Size(167, 28);
        helpToolStripMenuItem.Text = "Help";
        helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
        // 
        // reportToolStripMenuItem
        // 
        reportToolStripMenuItem.Name = "reportToolStripMenuItem";
        reportToolStripMenuItem.Size = new Size(167, 28);
        reportToolStripMenuItem.Text = "Report an Issue";
        reportToolStripMenuItem.Click += reportToolStripMenuItem_Click;
        // 
        // openLogsStripMenuItem
        // 
        openLogsStripMenuItem.Name = "openLogsStripMenuItem";
        openLogsStripMenuItem.Size = new Size(167, 28);
        openLogsStripMenuItem.Text = "Open Logs";
        openLogsStripMenuItem.Click += openLogsStripMenuItem_Click;
        // 
        // basePanel
        // 
        basePanel.BackColor = Color.FromArgb(239, 237, 230);
        basePanel.Controls.Add(layoutPanel);
        basePanel.Dock = DockStyle.Fill;
        basePanel.Location = new Point(0, 30);
        basePanel.Margin = new Padding(2, 2, 2, 2);
        basePanel.Name = "basePanel";
        basePanel.Padding = new Padding(21, 18, 21, 18);
        basePanel.Size = new Size(829, 727);
        basePanel.TabIndex = 16;
        // 
        // layoutPanel
        // 
        layoutPanel.ColumnCount = 1;
        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutPanel.Controls.Add(_repositoryDataGridView, 0, 1);
        layoutPanel.Controls.Add(searchRepositoryPanel, 0, 0);
        layoutPanel.Controls.Add(bottomPanel, 0, 2);
        layoutPanel.Dock = DockStyle.Fill;
        layoutPanel.Location = new Point(21, 18);
        layoutPanel.Margin = new Padding(2, 2, 2, 2);
        layoutPanel.Name = "layoutPanel";
        layoutPanel.RowCount = 3;
        layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
        layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 57.14286F));
        layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 42.85714F));
        layoutPanel.Size = new Size(787, 691);
        layoutPanel.TabIndex = 5;
        layoutPanel.Tag = "";
        // 
        // searchRepositoryPanel
        // 
        searchRepositoryPanel.Controls.Add(label1);
        searchRepositoryPanel.Controls.Add(operationGroupBox);
        searchRepositoryPanel.Dock = DockStyle.Fill;
        searchRepositoryPanel.Location = new Point(2, 2);
        searchRepositoryPanel.Margin = new Padding(2, 2, 2, 2);
        searchRepositoryPanel.Name = "searchRepositoryPanel";
        searchRepositoryPanel.Size = new Size(783, 146);
        searchRepositoryPanel.TabIndex = 5;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Dubai", 12F, FontStyle.Bold, GraphicsUnit.Point);
        label1.Location = new Point(3, 116);
        label1.Margin = new Padding(2, 0, 2, 0);
        label1.Name = "label1";
        label1.Size = new Size(63, 27);
        label1.TabIndex = 18;
        label1.Text = "Results";
        // 
        // operationGroupBox
        // 
        operationGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        operationGroupBox.Controls.Add(_huntButton);
        operationGroupBox.Controls.Add(_downloadButton);
        operationGroupBox.Controls.Add(_calculateMetricsButton);
        operationGroupBox.Font = new Font("Dubai", 12F, FontStyle.Bold, GraphicsUnit.Point);
        operationGroupBox.Location = new Point(147, 19);
        operationGroupBox.Margin = new Padding(2, 2, 2, 2);
        operationGroupBox.Name = "operationGroupBox";
        operationGroupBox.Padding = new Padding(2, 2, 2, 2);
        operationGroupBox.Size = new Size(504, 101);
        operationGroupBox.TabIndex = 17;
        operationGroupBox.TabStop = false;
        operationGroupBox.Text = "Operations";
        // 
        // _huntButton
        // 
        _huntButton.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _huntButton.ImageKey = "knife.png";
        _huntButton.ImageList = IconList;
        _huntButton.Location = new Point(386, 37);
        _huntButton.Margin = new Padding(2, 2, 2, 2);
        _huntButton.Name = "_huntButton";
        _huntButton.Size = new Size(85, 25);
        _huntButton.TabIndex = 8;
        _huntButton.Text = "Hunt";
        _huntButton.TextImageRelation = TextImageRelation.ImageBeforeText;
        _huntButton.UseVisualStyleBackColor = true;
        _huntButton.Click += huntButton_Click;
        // 
        // _downloadButton
        // 
        _downloadButton.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _downloadButton.ImageKey = "download.png";
        _downloadButton.ImageList = IconList;
        _downloadButton.Location = new Point(15, 37);
        _downloadButton.Margin = new Padding(2, 2, 2, 2);
        _downloadButton.Name = "_downloadButton";
        _downloadButton.Size = new Size(99, 25);
        _downloadButton.TabIndex = 6;
        _downloadButton.Text = "Download";
        _downloadButton.TextImageRelation = TextImageRelation.ImageBeforeText;
        _downloadButton.UseVisualStyleBackColor = true;
        _downloadButton.Click += _downloadButton_Click;
        // 
        // _calculateMetricsButton
        // 
        _calculateMetricsButton.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _calculateMetricsButton.ImageKey = "calculation.png";
        _calculateMetricsButton.ImageList = IconList;
        _calculateMetricsButton.Location = new Point(173, 37);
        _calculateMetricsButton.Margin = new Padding(2, 2, 2, 2);
        _calculateMetricsButton.Name = "_calculateMetricsButton";
        _calculateMetricsButton.Size = new Size(149, 25);
        _calculateMetricsButton.TabIndex = 7;
        _calculateMetricsButton.Text = "Calculate Metrics";
        _calculateMetricsButton.TextImageRelation = TextImageRelation.ImageBeforeText;
        _calculateMetricsButton.UseVisualStyleBackColor = true;
        _calculateMetricsButton.Click += _calculateMetricsButton_Click;
        // 
        // bottomPanel
        // 
        bottomPanel.Controls.Add(bottomLayoutPanel);
        bottomPanel.Dock = DockStyle.Fill;
        bottomPanel.Location = new Point(2, 461);
        bottomPanel.Margin = new Padding(2, 2, 2, 2);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Size = new Size(783, 228);
        bottomPanel.TabIndex = 6;
        // 
        // bottomLayoutPanel
        // 
        bottomLayoutPanel.ColumnCount = 1;
        bottomLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        bottomLayoutPanel.Controls.Add(logTextBox, 0, 0);
        bottomLayoutPanel.Controls.Add(panel1, 2, 1);
        bottomLayoutPanel.Dock = DockStyle.Fill;
        bottomLayoutPanel.Location = new Point(0, 0);
        bottomLayoutPanel.Margin = new Padding(2, 2, 2, 2);
        bottomLayoutPanel.Name = "bottomLayoutPanel";
        bottomLayoutPanel.RowCount = 2;
        bottomLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bottomLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
        bottomLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));
        bottomLayoutPanel.Size = new Size(783, 228);
        bottomLayoutPanel.TabIndex = 0;
        // 
        // logTextBox
        // 
        logTextBox.Dock = DockStyle.Fill;
        logTextBox.Font = new Font("Dubai", 12F, FontStyle.Bold, GraphicsUnit.Point);
        logTextBox.Location = new Point(2, 2);
        logTextBox.Margin = new Padding(2, 2, 2, 2);
        logTextBox.Name = "logTextBox";
        logTextBox.ReadOnly = true;
        logTextBox.Size = new Size(779, 209);
        logTextBox.TabIndex = 19;
        logTextBox.Text = "";
        // 
        // panel1
        // 
        panel1.Controls.Add(_cancelButton);
        panel1.Controls.Add(_progressBar);
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(2, 215);
        panel1.Margin = new Padding(2, 2, 2, 2);
        panel1.Name = "panel1";
        panel1.Size = new Size(779, 11);
        panel1.TabIndex = 20;
        // 
        // _cancelButton
        // 
        _cancelButton.Anchor = AnchorStyles.Right;
        _cancelButton.ImageKey = "close.png";
        _cancelButton.ImageList = IconList;
        _cancelButton.Location = new Point(751, -5);
        _cancelButton.Margin = new Padding(2, 2, 2, 2);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.Size = new Size(28, 21);
        _cancelButton.TabIndex = 19;
        _cancelButton.UseVisualStyleBackColor = true;
        _cancelButton.Click += cancelButton_Click;
        // 
        // _progressBar
        // 
        _progressBar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _progressBar.Location = new Point(0, -5);
        _progressBar.Margin = new Padding(2, 2, 2, 2);
        _progressBar.Name = "_progressBar";
        _progressBar.Size = new Size(755, 20);
        _progressBar.TabIndex = 18;
        // 
        // ViewMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(115, 186, 155);
        ClientSize = new Size(829, 757);
        Controls.Add(basePanel);
        Controls.Add(menu);
        ForeColor = Color.Black;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = menu;
        Margin = new Padding(2, 2, 2, 2);
        MinimumSize = new Size(845, 796);
        Name = "ViewMain";
        Text = "MetricHunter";
        Load += _viewMain_Load;
        ((System.ComponentModel.ISupportInitialize)_repositoryDataGridView).EndInit();
        menu.ResumeLayout(false);
        menu.PerformLayout();
        basePanel.ResumeLayout(false);
        layoutPanel.ResumeLayout(false);
        searchRepositoryPanel.ResumeLayout(false);
        searchRepositoryPanel.PerformLayout();
        operationGroupBox.ResumeLayout(false);
        bottomPanel.ResumeLayout(false);
        bottomLayoutPanel.ResumeLayout(false);
        panel1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private DataGridView _repositoryDataGridView;
    private MenuStrip menu;
    private ToolStripMenuItem helpMenu;
    private Panel basePanel;
    private TableLayoutPanel layoutPanel;
    private ToolStripMenuItem contributorsToolStripMenuItem;
    private ToolStripMenuItem reportToolStripMenuItem;
    private ToolStripMenuItem savedRepositoriesToolStripMenuItem;
    private ToolStripMenuItem loginGithubToolStripMenuItem;
    private ToolStripMenuItem showToolStripMenuItem;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ImageList IconList;
    private ImageList SearchButton;
    private ToolStripMenuItem helpToolStripMenuItem;
    private Panel bottomPanel;
    private GroupBox operationGroupBox;
    private Button _huntButton;
    private Button _downloadButton;
    private Button _calculateMetricsButton;
    private ToolStripMenuItem openLogsStripMenuItem;
    private Panel searchRepositoryPanel;
    private TableLayoutPanel bottomLayoutPanel;
    private RichTextBox logTextBox;
    private Panel panel1;
    private Button _cancelButton;
    private ProgressBar _progressBar;
    private ToolStripMenuItem exploreRepositoriesToolStripMenuItem;
    private ToolStripMenuItem findRepositoryToolStripMenuItem;
    private Label label1;
}
