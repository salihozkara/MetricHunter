namespace MetricHunter.Desktop;

partial class ViewMain
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.SearchButton = new System.Windows.Forms.ImageList(this.components);
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this._repositoryDataGridView = new System.Windows.Forms.DataGridView();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.savedRepositoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginGithubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contributorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basePanel = new System.Windows.Forms.Panel();
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.searchRepositoryPanel = new System.Windows.Forms.Panel();
            this.operationGroupBox = new System.Windows.Forms.GroupBox();
            this._huntButton = new System.Windows.Forms.Button();
            this._downloadButton = new System.Windows.Forms.Button();
            this._calculateMetricsButton = new System.Windows.Forms.Button();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.bottomLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._cancelButton = new System.Windows.Forms.Button();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this.exploreRepositoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findRepositoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this._repositoryDataGridView)).BeginInit();
            this.menu.SuspendLayout();
            this.basePanel.SuspendLayout();
            this.layoutPanel.SuspendLayout();
            this.searchRepositoryPanel.SuspendLayout();
            this.operationGroupBox.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.bottomLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchButton
            // 
            this.SearchButton.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.SearchButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SearchButton.ImageStream")));
            this.SearchButton.TransparentColor = System.Drawing.Color.Transparent;
            this.SearchButton.Images.SetKeyName(0, "search.png");
            // 
            // IconList
            // 
            this.IconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
            this.IconList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconList.Images.SetKeyName(0, "search.png");
            this.IconList.Images.SetKeyName(1, "knife.png");
            this.IconList.Images.SetKeyName(2, "calculation.png");
            this.IconList.Images.SetKeyName(3, "download.png");
            this.IconList.Images.SetKeyName(4, "close.png");
            // 
            // _repositoryDataGridView
            // 
            this._repositoryDataGridView.AllowUserToAddRows = false;
            this._repositoryDataGridView.AllowUserToDeleteRows = false;
            this._repositoryDataGridView.AllowUserToResizeColumns = false;
            this._repositoryDataGridView.AllowUserToResizeRows = false;
            this._repositoryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._repositoryDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this._repositoryDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(211)))), ((int)(((byte)(193)))));
            this._repositoryDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._repositoryDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(129)))), ((int)(((byte)(124)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._repositoryDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this._repositoryDataGridView.ColumnHeadersHeight = 70;
            this._repositoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._repositoryDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this._repositoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._repositoryDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._repositoryDataGridView.Location = new System.Drawing.Point(3, 203);
            this._repositoryDataGridView.Name = "_repositoryDataGridView";
            this._repositoryDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(129)))), ((int)(((byte)(124)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._repositoryDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this._repositoryDataGridView.RowHeadersWidth = 45;
            this._repositoryDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._repositoryDataGridView.RowTemplate.Height = 50;
            this._repositoryDataGridView.Size = new System.Drawing.Size(1098, 544);
            this._repositoryDataGridView.TabIndex = 4;
            this._repositoryDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._repositoryDataGridView_CellContentClick);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(211)))), ((int)(((byte)(193)))));
            this.menu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savedRepositoriesToolStripMenuItem,
            this.loginGithubToolStripMenuItem,
            this.helpMenu});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1164, 42);
            this.menu.TabIndex = 15;
            this.menu.Text = "menuStrip1";
            // 
            // savedRepositoriesToolStripMenuItem
            // 
            this.savedRepositoriesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exploreRepositoriesToolStripMenuItem,
            this.findRepositoryToolStripMenuItem,
            this.showToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.savedRepositoriesToolStripMenuItem.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.savedRepositoriesToolStripMenuItem.Name = "savedRepositoriesToolStripMenuItem";
            this.savedRepositoriesToolStripMenuItem.Size = new System.Drawing.Size(118, 38);
            this.savedRepositoriesToolStripMenuItem.Text = "Repository";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(286, 42);
            this.showToolStripMenuItem.Text = "Load";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(286, 42);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loginGithubToolStripMenuItem
            // 
            this.loginGithubToolStripMenuItem.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loginGithubToolStripMenuItem.Name = "loginGithubToolStripMenuItem";
            this.loginGithubToolStripMenuItem.Size = new System.Drawing.Size(140, 38);
            this.loginGithubToolStripMenuItem.Text = "Login GitHub";
            this.loginGithubToolStripMenuItem.Click += new System.EventHandler(this.loginGithubToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contributorsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.openLogsStripMenuItem});
            this.helpMenu.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(69, 38);
            this.helpMenu.Text = "Help";
            // 
            // contributorsToolStripMenuItem
            // 
            this.contributorsToolStripMenuItem.Name = "contributorsToolStripMenuItem";
            this.contributorsToolStripMenuItem.Size = new System.Drawing.Size(244, 42);
            this.contributorsToolStripMenuItem.Text = "Contributors";
            this.contributorsToolStripMenuItem.Click += new System.EventHandler(this.contributorsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(244, 42);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(244, 42);
            this.reportToolStripMenuItem.Text = "Report an Issue";
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // openLogsStripMenuItem
            // 
            this.openLogsStripMenuItem.Name = "openLogsStripMenuItem";
            this.openLogsStripMenuItem.Size = new System.Drawing.Size(244, 42);
            this.openLogsStripMenuItem.Text = "Open Logs";
            this.openLogsStripMenuItem.Click += new System.EventHandler(this.openLogsStripMenuItem_Click);
            // 
            // basePanel
            // 
            this.basePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            this.basePanel.Controls.Add(this.layoutPanel);
            this.basePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basePanel.Location = new System.Drawing.Point(0, 42);
            this.basePanel.Name = "basePanel";
            this.basePanel.Padding = new System.Windows.Forms.Padding(30);
            this.basePanel.Size = new System.Drawing.Size(1164, 1178);
            this.basePanel.TabIndex = 16;
            // 
            // layoutPanel
            // 
            this.layoutPanel.ColumnCount = 1;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Controls.Add(this._repositoryDataGridView, 0, 1);
            this.layoutPanel.Controls.Add(this.searchRepositoryPanel, 0, 0);
            this.layoutPanel.Controls.Add(this.bottomPanel, 0, 2);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(30, 30);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 3;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutPanel.Size = new System.Drawing.Size(1104, 1118);
            this.layoutPanel.TabIndex = 5;
            // 
            // searchRepositoryPanel
            // 
            this.searchRepositoryPanel.Controls.Add(this.operationGroupBox);
            this.searchRepositoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchRepositoryPanel.Location = new System.Drawing.Point(3, 3);
            this.searchRepositoryPanel.Name = "searchRepositoryPanel";
            this.searchRepositoryPanel.Size = new System.Drawing.Size(1098, 194);
            this.searchRepositoryPanel.TabIndex = 5;
            // 
            // operationGroupBox
            // 
            this.operationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.operationGroupBox.Controls.Add(this._huntButton);
            this.operationGroupBox.Controls.Add(this._downloadButton);
            this.operationGroupBox.Controls.Add(this._calculateMetricsButton);
            this.operationGroupBox.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operationGroupBox.Location = new System.Drawing.Point(209, 23);
            this.operationGroupBox.Name = "operationGroupBox";
            this.operationGroupBox.Size = new System.Drawing.Size(701, 144);
            this.operationGroupBox.TabIndex = 17;
            this.operationGroupBox.TabStop = false;
            this.operationGroupBox.Text = "Operations";
            // 
            // _huntButton
            // 
            this._huntButton.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._huntButton.ImageKey = "knife.png";
            this._huntButton.ImageList = this.IconList;
            this._huntButton.Location = new System.Drawing.Point(552, 61);
            this._huntButton.Name = "_huntButton";
            this._huntButton.Size = new System.Drawing.Size(121, 41);
            this._huntButton.TabIndex = 8;
            this._huntButton.Text = "Hunt";
            this._huntButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._huntButton.UseVisualStyleBackColor = true;
            this._huntButton.Click += new System.EventHandler(this.huntButton_Click);
            // 
            // _downloadButton
            // 
            this._downloadButton.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._downloadButton.ImageKey = "download.png";
            this._downloadButton.ImageList = this.IconList;
            this._downloadButton.Location = new System.Drawing.Point(22, 61);
            this._downloadButton.Name = "_downloadButton";
            this._downloadButton.Size = new System.Drawing.Size(141, 41);
            this._downloadButton.TabIndex = 6;
            this._downloadButton.Text = "Download";
            this._downloadButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._downloadButton.UseVisualStyleBackColor = true;
            this._downloadButton.Click += new System.EventHandler(this._downloadButton_Click);
            // 
            // _calculateMetricsButton
            // 
            this._calculateMetricsButton.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._calculateMetricsButton.ImageKey = "calculation.png";
            this._calculateMetricsButton.ImageList = this.IconList;
            this._calculateMetricsButton.Location = new System.Drawing.Point(247, 61);
            this._calculateMetricsButton.Name = "_calculateMetricsButton";
            this._calculateMetricsButton.Size = new System.Drawing.Size(213, 41);
            this._calculateMetricsButton.TabIndex = 7;
            this._calculateMetricsButton.Text = "Calculate Metrics";
            this._calculateMetricsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._calculateMetricsButton.UseVisualStyleBackColor = true;
            this._calculateMetricsButton.Click += new System.EventHandler(this._calculateMetricsButton_Click);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.bottomLayoutPanel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(3, 753);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(1098, 362);
            this.bottomPanel.TabIndex = 6;
            // 
            // bottomLayoutPanel
            // 
            this.bottomLayoutPanel.ColumnCount = 1;
            this.bottomLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottomLayoutPanel.Controls.Add(this.logTextBox, 0, 0);
            this.bottomLayoutPanel.Controls.Add(this.panel1, 2, 1);
            this.bottomLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.bottomLayoutPanel.Name = "bottomLayoutPanel";
            this.bottomLayoutPanel.RowCount = 2;
            this.bottomLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottomLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.bottomLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.bottomLayoutPanel.Size = new System.Drawing.Size(1098, 362);
            this.bottomLayoutPanel.TabIndex = 0;
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.logTextBox.Location = new System.Drawing.Point(3, 3);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(1092, 331);
            this.logTextBox.TabIndex = 19;
            this.logTextBox.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._cancelButton);
            this.panel1.Controls.Add(this._progressBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 340);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1092, 19);
            this.panel1.TabIndex = 20;
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._cancelButton.ImageKey = "close.png";
            this._cancelButton.ImageList = this.IconList;
            this._cancelButton.Location = new System.Drawing.Point(1052, -8);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(40, 35);
            this._cancelButton.TabIndex = 19;
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._progressBar.Location = new System.Drawing.Point(0, -8);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(1057, 34);
            this._progressBar.TabIndex = 18;
            // 
            // exploreRepositoriesToolStripMenuItem
            // 
            this.exploreRepositoriesToolStripMenuItem.Name = "exploreRepositoriesToolStripMenuItem";
            this.exploreRepositoriesToolStripMenuItem.Size = new System.Drawing.Size(286, 42);
            this.exploreRepositoriesToolStripMenuItem.Text = "Explore Repositories";
            this.exploreRepositoriesToolStripMenuItem.Click += new System.EventHandler(this.exploreRepositoriesToolStripMenuItem_Click);
            // 
            // findRepositoryToolStripMenuItem
            // 
            this.findRepositoryToolStripMenuItem.Name = "findRepositoryToolStripMenuItem";
            this.findRepositoryToolStripMenuItem.Size = new System.Drawing.Size(286, 42);
            this.findRepositoryToolStripMenuItem.Text = "Find Repository";
            this.findRepositoryToolStripMenuItem.Click += new System.EventHandler(this.findRepositoryToolStripMenuItem_Click);
            // 
            // ViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(186)))), ((int)(((byte)(155)))));
            this.ClientSize = new System.Drawing.Size(1164, 1220);
            this.Controls.Add(this.basePanel);
            this.Controls.Add(this.menu);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(897, 806);
            this.Name = "ViewMain";
            this.Text = "MetricHunter";
            this.Load += new System.EventHandler(this._viewMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this._repositoryDataGridView)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.basePanel.ResumeLayout(false);
            this.layoutPanel.ResumeLayout(false);
            this.searchRepositoryPanel.ResumeLayout(false);
            this.operationGroupBox.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.bottomLayoutPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
}
