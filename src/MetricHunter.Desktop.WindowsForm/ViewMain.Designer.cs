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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._languageComboBox = new System.Windows.Forms.ComboBox();
            this._topicsTextBox = new System.Windows.Forms.TextBox();
            this._repositoryCountTextBox = new System.Windows.Forms.TextBox();
            this._sortDirectionComboBox = new System.Windows.Forms.ComboBox();
            this._searchButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.ImageList(this.components);
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this._topicsLabel = new System.Windows.Forms.Label();
            this._countRepositoryLabel = new System.Windows.Forms.Label();
            this._languageLabel = new System.Windows.Forms.Label();
            this._sortDirectionLabel = new System.Windows.Forms.Label();
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
            this.searchGroupBox = new System.Windows.Forms.GroupBox();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.bottomLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.operationGroupBox = new System.Windows.Forms.GroupBox();
            this._huntButton = new System.Windows.Forms.Button();
            this._downloadButton = new System.Windows.Forms.Button();
            this._calculateMetricsButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this._cancelButton = new System.Windows.Forms.Button();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this._repositoryDataGridView)).BeginInit();
            this.menu.SuspendLayout();
            this.basePanel.SuspendLayout();
            this.layoutPanel.SuspendLayout();
            this.searchRepositoryPanel.SuspendLayout();
            this.searchGroupBox.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.bottomLayoutPanel.SuspendLayout();
            this.operationGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _languageComboBox
            // 
            this._languageComboBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._languageComboBox.FormattingEnabled = true;
            this._languageComboBox.Location = new System.Drawing.Point(370, 74);
            this._languageComboBox.Name = "_languageComboBox";
            this._languageComboBox.Size = new System.Drawing.Size(142, 42);
            this._languageComboBox.TabIndex = 0;
            // 
            // _topicsTextBox
            // 
            this._topicsTextBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._topicsTextBox.Location = new System.Drawing.Point(45, 74);
            this._topicsTextBox.Name = "_topicsTextBox";
            this._topicsTextBox.Size = new System.Drawing.Size(142, 41);
            this._topicsTextBox.TabIndex = 1;
            // 
            // _repositoryCountTextBox
            // 
            this._repositoryCountTextBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._repositoryCountTextBox.Location = new System.Drawing.Point(205, 74);
            this._repositoryCountTextBox.Name = "_repositoryCountTextBox";
            this._repositoryCountTextBox.Size = new System.Drawing.Size(142, 41);
            this._repositoryCountTextBox.TabIndex = 2;
            // 
            // _sortDirectionComboBox
            // 
            this._sortDirectionComboBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._sortDirectionComboBox.FormattingEnabled = true;
            this._sortDirectionComboBox.Location = new System.Drawing.Point(534, 74);
            this._sortDirectionComboBox.Name = "_sortDirectionComboBox";
            this._sortDirectionComboBox.Size = new System.Drawing.Size(142, 42);
            this._sortDirectionComboBox.TabIndex = 3;
            // 
            // _searchButton
            // 
            this._searchButton.ImageKey = "search.png";
            this._searchButton.ImageList = this.SearchButton;
            this._searchButton.Location = new System.Drawing.Point(693, 74);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(48, 41);
            this._searchButton.TabIndex = 5;
            this._searchButton.UseVisualStyleBackColor = true;
            this._searchButton.Click += new System.EventHandler(this._searchButton_Click);
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
            // _topicsLabel
            // 
            this._topicsLabel.AutoSize = true;
            this._topicsLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._topicsLabel.Location = new System.Drawing.Point(45, 37);
            this._topicsLabel.Name = "_topicsLabel";
            this._topicsLabel.Size = new System.Drawing.Size(68, 34);
            this._topicsLabel.TabIndex = 8;
            this._topicsLabel.Text = "Topics";
            // 
            // _countRepositoryLabel
            // 
            this._countRepositoryLabel.AutoSize = true;
            this._countRepositoryLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._countRepositoryLabel.Location = new System.Drawing.Point(205, 37);
            this._countRepositoryLabel.Name = "_countRepositoryLabel";
            this._countRepositoryLabel.Size = new System.Drawing.Size(63, 34);
            this._countRepositoryLabel.TabIndex = 9;
            this._countRepositoryLabel.Text = "Count";
            // 
            // _languageLabel
            // 
            this._languageLabel.AutoSize = true;
            this._languageLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._languageLabel.Location = new System.Drawing.Point(370, 37);
            this._languageLabel.Name = "_languageLabel";
            this._languageLabel.Size = new System.Drawing.Size(92, 34);
            this._languageLabel.TabIndex = 10;
            this._languageLabel.Text = "Language";
            // 
            // _sortDirectionLabel
            // 
            this._sortDirectionLabel.AutoSize = true;
            this._sortDirectionLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._sortDirectionLabel.Location = new System.Drawing.Point(534, 37);
            this._sortDirectionLabel.Name = "_sortDirectionLabel";
            this._sortDirectionLabel.Size = new System.Drawing.Size(123, 34);
            this._sortDirectionLabel.TabIndex = 11;
            this._sortDirectionLabel.Text = "Sort By Stars";
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(129)))), ((int)(((byte)(124)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._repositoryDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._repositoryDataGridView.ColumnHeadersHeight = 70;
            this._repositoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._repositoryDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this._repositoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._repositoryDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._repositoryDataGridView.Location = new System.Drawing.Point(3, 153);
            this._repositoryDataGridView.Name = "_repositoryDataGridView";
            this._repositoryDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(129)))), ((int)(((byte)(124)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._repositoryDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this._repositoryDataGridView.RowHeadersWidth = 30;
            this._repositoryDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._repositoryDataGridView.RowTemplate.Height = 50;
            this._repositoryDataGridView.Size = new System.Drawing.Size(1112, 589);
            this._repositoryDataGridView.TabIndex = 4;
            this._repositoryDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._repositoryDataGridView_CellClick);
            this._repositoryDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._repositoryDataGridView_CellContentClick);
            this._repositoryDataGridView.SelectionChanged += new System.EventHandler(this._repositoryDataGridView_SelectionChanged);
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
            this.menu.Size = new System.Drawing.Size(1178, 42);
            this.menu.TabIndex = 15;
            this.menu.Text = "menuStrip1";
            // 
            // savedRepositoriesToolStripMenuItem
            // 
            this.savedRepositoriesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.showToolStripMenuItem.Size = new System.Drawing.Size(159, 42);
            this.showToolStripMenuItem.Text = "Load";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(159, 42);
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
            this.basePanel.Size = new System.Drawing.Size(1178, 1202);
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
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutPanel.Size = new System.Drawing.Size(1118, 1142);
            this.layoutPanel.TabIndex = 5;
            // 
            // searchRepositoryPanel
            // 
            this.searchRepositoryPanel.Controls.Add(this.searchGroupBox);
            this.searchRepositoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchRepositoryPanel.Location = new System.Drawing.Point(3, 3);
            this.searchRepositoryPanel.Name = "searchRepositoryPanel";
            this.searchRepositoryPanel.Size = new System.Drawing.Size(1112, 144);
            this.searchRepositoryPanel.TabIndex = 5;
            // 
            // searchGroupBox
            // 
            this.searchGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.searchGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            this.searchGroupBox.Controls.Add(this._repositoryCountTextBox);
            this.searchGroupBox.Controls.Add(this._sortDirectionLabel);
            this.searchGroupBox.Controls.Add(this._searchButton);
            this.searchGroupBox.Controls.Add(this._languageLabel);
            this.searchGroupBox.Controls.Add(this._sortDirectionComboBox);
            this.searchGroupBox.Controls.Add(this._countRepositoryLabel);
            this.searchGroupBox.Controls.Add(this._languageComboBox);
            this.searchGroupBox.Controls.Add(this._topicsTextBox);
            this.searchGroupBox.Controls.Add(this._topicsLabel);
            this.searchGroupBox.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.searchGroupBox.Location = new System.Drawing.Point(153, 13);
            this.searchGroupBox.Name = "searchGroupBox";
            this.searchGroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.searchGroupBox.Size = new System.Drawing.Size(788, 128);
            this.searchGroupBox.TabIndex = 12;
            this.searchGroupBox.TabStop = false;
            this.searchGroupBox.Text = "Search Repositories";
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.bottomLayoutPanel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(3, 748);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(1112, 391);
            this.bottomPanel.TabIndex = 6;
            // 
            // bottomLayoutPanel
            // 
            this.bottomLayoutPanel.ColumnCount = 1;
            this.bottomLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottomLayoutPanel.Controls.Add(this.logTextBox, 0, 1);
            this.bottomLayoutPanel.Controls.Add(this.operationGroupBox, 0, 0);
            this.bottomLayoutPanel.Controls.Add(this.panel1, 2, 2);
            this.bottomLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.bottomLayoutPanel.Name = "bottomLayoutPanel";
            this.bottomLayoutPanel.RowCount = 3;
            this.bottomLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.bottomLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottomLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.bottomLayoutPanel.Size = new System.Drawing.Size(1112, 391);
            this.bottomLayoutPanel.TabIndex = 0;
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.logTextBox.Location = new System.Drawing.Point(3, 153);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(1106, 210);
            this.logTextBox.TabIndex = 19;
            this.logTextBox.Text = "";
            // 
            // operationGroupBox
            // 
            this.operationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.operationGroupBox.Controls.Add(this._huntButton);
            this.operationGroupBox.Controls.Add(this._downloadButton);
            this.operationGroupBox.Controls.Add(this._calculateMetricsButton);
            this.operationGroupBox.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operationGroupBox.Location = new System.Drawing.Point(205, 3);
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
            // panel1
            // 
            this.panel1.Controls.Add(this._cancelButton);
            this.panel1.Controls.Add(this._progressBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1106, 19);
            this.panel1.TabIndex = 20;
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._cancelButton.ImageKey = "close.png";
            this._cancelButton.ImageList = this.IconList;
            this._cancelButton.Location = new System.Drawing.Point(1066, -8);
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
            this._progressBar.Size = new System.Drawing.Size(1071, 34);
            this._progressBar.TabIndex = 18;
            // 
            // ViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(186)))), ((int)(((byte)(155)))));
            this.ClientSize = new System.Drawing.Size(1178, 1244);
            this.Controls.Add(this.basePanel);
            this.Controls.Add(this.menu);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(1200, 1300);
            this.Name = "ViewMain";
            this.Text = "MetricHunter";
            this.Load += new System.EventHandler(this._viewMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this._repositoryDataGridView)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.basePanel.ResumeLayout(false);
            this.layoutPanel.ResumeLayout(false);
            this.searchRepositoryPanel.ResumeLayout(false);
            this.searchGroupBox.ResumeLayout(false);
            this.searchGroupBox.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.bottomLayoutPanel.ResumeLayout(false);
            this.operationGroupBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private ComboBox _languageComboBox;
    private TextBox _topicsTextBox;
    private TextBox _repositoryCountTextBox;
    private ComboBox _sortDirectionComboBox;
    private Button _searchButton;
    private Label _topicsLabel;
    private Label _countRepositoryLabel;
    private Label _languageLabel;
    private Label _sortDirectionLabel;
    private DataGridView _repositoryDataGridView;
    private MenuStrip menu;
    private ToolStripMenuItem helpMenu;
    private Panel basePanel;
    private TableLayoutPanel layoutPanel;
    private Panel searchRepositoryPanel;
    private GroupBox searchGroupBox;
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
    private TableLayoutPanel bottomLayoutPanel;
    private ProgressBar _progressBar;
    private RichTextBox logTextBox;
    private GroupBox operationGroupBox;
    private Button _huntButton;
    private Button _downloadButton;
    private Button _calculateMetricsButton;
    private ToolStripMenuItem openLogsStripMenuItem;
    private Panel panel1;
    private Button _cancelButton;
}
