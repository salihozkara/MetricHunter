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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this._languageComboBox = new System.Windows.Forms.ComboBox();
            this._topicsTextBox = new System.Windows.Forms.TextBox();
            this._repositoryCountTextBox = new System.Windows.Forms.TextBox();
            this._sortDirectionComboBox = new System.Windows.Forms.ComboBox();
            this._searchButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.ImageList(this.components);
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this._downloadButton = new System.Windows.Forms.Button();
            this._calculateMetricsButton = new System.Windows.Forms.Button();
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
            this.basePanel = new System.Windows.Forms.Panel();
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.searchRepositoryPanel = new System.Windows.Forms.Panel();
            this.searchGroupBox = new System.Windows.Forms.GroupBox();
            this.operationGroupBox = new System.Windows.Forms.GroupBox();
            this.huntButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._repositoryDataGridView)).BeginInit();
            this.menu.SuspendLayout();
            this.basePanel.SuspendLayout();
            this.layoutPanel.SuspendLayout();
            this.searchRepositoryPanel.SuspendLayout();
            this.searchGroupBox.SuspendLayout();
            this.operationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _languageComboBox
            // 
            this._languageComboBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._languageComboBox.FormattingEnabled = true;
            this._languageComboBox.Location = new System.Drawing.Point(339, 81);
            this._languageComboBox.Name = "_languageComboBox";
            this._languageComboBox.Size = new System.Drawing.Size(142, 42);
            this._languageComboBox.TabIndex = 0;
            // 
            // _topicsTextBox
            // 
            this._topicsTextBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._topicsTextBox.Location = new System.Drawing.Point(14, 81);
            this._topicsTextBox.Name = "_topicsTextBox";
            this._topicsTextBox.Size = new System.Drawing.Size(142, 41);
            this._topicsTextBox.TabIndex = 1;
            // 
            // _repositoryCountTextBox
            // 
            this._repositoryCountTextBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._repositoryCountTextBox.Location = new System.Drawing.Point(174, 81);
            this._repositoryCountTextBox.Name = "_repositoryCountTextBox";
            this._repositoryCountTextBox.Size = new System.Drawing.Size(142, 41);
            this._repositoryCountTextBox.TabIndex = 2;
            // 
            // _sortDirectionComboBox
            // 
            this._sortDirectionComboBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._sortDirectionComboBox.FormattingEnabled = true;
            this._sortDirectionComboBox.Location = new System.Drawing.Point(503, 81);
            this._sortDirectionComboBox.Name = "_sortDirectionComboBox";
            this._sortDirectionComboBox.Size = new System.Drawing.Size(142, 42);
            this._sortDirectionComboBox.TabIndex = 3;
            // 
            // _searchButton
            // 
            this._searchButton.ImageKey = "search.png";
            this._searchButton.ImageList = this.SearchButton;
            this._searchButton.Location = new System.Drawing.Point(662, 81);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(48, 42);
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
            // 
            // _downloadButton
            // 
            this._downloadButton.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._downloadButton.ImageKey = "download.png";
            this._downloadButton.ImageList = this.IconList;
            this._downloadButton.Location = new System.Drawing.Point(18, 47);
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
            this._calculateMetricsButton.Location = new System.Drawing.Point(246, 47);
            this._calculateMetricsButton.Name = "_calculateMetricsButton";
            this._calculateMetricsButton.Size = new System.Drawing.Size(213, 41);
            this._calculateMetricsButton.TabIndex = 7;
            this._calculateMetricsButton.Text = "Calculate Metrics";
            this._calculateMetricsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._calculateMetricsButton.UseVisualStyleBackColor = true;
            this._calculateMetricsButton.Click += new System.EventHandler(this._calculateMetricsButton_Click);
            // 
            // _topicsLabel
            // 
            this._topicsLabel.AutoSize = true;
            this._topicsLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._topicsLabel.Location = new System.Drawing.Point(14, 44);
            this._topicsLabel.Name = "_topicsLabel";
            this._topicsLabel.Size = new System.Drawing.Size(68, 34);
            this._topicsLabel.TabIndex = 8;
            this._topicsLabel.Text = "Topics";
            // 
            // _countRepositoryLabel
            // 
            this._countRepositoryLabel.AutoSize = true;
            this._countRepositoryLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._countRepositoryLabel.Location = new System.Drawing.Point(174, 44);
            this._countRepositoryLabel.Name = "_countRepositoryLabel";
            this._countRepositoryLabel.Size = new System.Drawing.Size(63, 34);
            this._countRepositoryLabel.TabIndex = 9;
            this._countRepositoryLabel.Text = "Count";
            // 
            // _languageLabel
            // 
            this._languageLabel.AutoSize = true;
            this._languageLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._languageLabel.Location = new System.Drawing.Point(339, 44);
            this._languageLabel.Name = "_languageLabel";
            this._languageLabel.Size = new System.Drawing.Size(92, 34);
            this._languageLabel.TabIndex = 10;
            this._languageLabel.Text = "Language";
            // 
            // _sortDirectionLabel
            // 
            this._sortDirectionLabel.AutoSize = true;
            this._sortDirectionLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._sortDirectionLabel.Location = new System.Drawing.Point(503, 44);
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(129)))), ((int)(((byte)(124)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._repositoryDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this._repositoryDataGridView.ColumnHeadersHeight = 70;
            this._repositoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._repositoryDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this._repositoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._repositoryDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._repositoryDataGridView.Location = new System.Drawing.Point(3, 153);
            this._repositoryDataGridView.Name = "_repositoryDataGridView";
            this._repositoryDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(129)))), ((int)(((byte)(124)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._repositoryDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this._repositoryDataGridView.RowHeadersWidth = 45;
            this._repositoryDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._repositoryDataGridView.RowTemplate.Height = 50;
            this._repositoryDataGridView.Size = new System.Drawing.Size(1098, 561);
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
            this.reportToolStripMenuItem});
            this.helpMenu.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(69, 38);
            this.helpMenu.Text = "Help";
            // 
            // contributorsToolStripMenuItem
            // 
            this.contributorsToolStripMenuItem.Name = "contributorsToolStripMenuItem";
            this.contributorsToolStripMenuItem.Size = new System.Drawing.Size(270, 42);
            this.contributorsToolStripMenuItem.Text = "Contributors";
            this.contributorsToolStripMenuItem.Click += new System.EventHandler(this.contributorsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(270, 42);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(270, 42);
            this.reportToolStripMenuItem.Text = "Report an Issue";
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // basePanel
            // 
            this.basePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
            this.basePanel.Controls.Add(this.layoutPanel);
            this.basePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basePanel.Location = new System.Drawing.Point(0, 42);
            this.basePanel.Name = "basePanel";
            this.basePanel.Padding = new System.Windows.Forms.Padding(30);
            this.basePanel.Size = new System.Drawing.Size(1164, 927);
            this.basePanel.TabIndex = 16;
            // 
            // layoutPanel
            // 
            this.layoutPanel.ColumnCount = 1;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Controls.Add(this._repositoryDataGridView, 0, 1);
            this.layoutPanel.Controls.Add(this.searchRepositoryPanel, 0, 0);
            this.layoutPanel.Controls.Add(this.operationGroupBox, 0, 2);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(30, 30);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 3;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.layoutPanel.Size = new System.Drawing.Size(1104, 867);
            this.layoutPanel.TabIndex = 5;
            // 
            // searchRepositoryPanel
            // 
            this.searchRepositoryPanel.Controls.Add(this.searchGroupBox);
            this.searchRepositoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchRepositoryPanel.Location = new System.Drawing.Point(3, 3);
            this.searchRepositoryPanel.Name = "searchRepositoryPanel";
            this.searchRepositoryPanel.Size = new System.Drawing.Size(1098, 144);
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
            this.searchGroupBox.Location = new System.Drawing.Point(144, 3);
            this.searchGroupBox.Name = "searchGroupBox";
            this.searchGroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.searchGroupBox.Size = new System.Drawing.Size(804, 138);
            this.searchGroupBox.TabIndex = 12;
            this.searchGroupBox.TabStop = false;
            this.searchGroupBox.Text = "Search Repositories";
            // 
            // operationGroupBox
            // 
            this.operationGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.operationGroupBox.Controls.Add(this.huntButton);
            this.operationGroupBox.Controls.Add(this._downloadButton);
            this.operationGroupBox.Controls.Add(this._calculateMetricsButton);
            this.operationGroupBox.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operationGroupBox.Location = new System.Drawing.Point(201, 745);
            this.operationGroupBox.Name = "operationGroupBox";
            this.operationGroupBox.Size = new System.Drawing.Size(701, 94);
            this.operationGroupBox.TabIndex = 6;
            this.operationGroupBox.TabStop = false;
            this.operationGroupBox.Text = "Operations";
            // 
            // huntButton
            // 
            this.huntButton.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.huntButton.ImageKey = "knife.png";
            this.huntButton.ImageList = this.IconList;
            this.huntButton.Location = new System.Drawing.Point(557, 47);
            this.huntButton.Name = "huntButton";
            this.huntButton.Size = new System.Drawing.Size(121, 41);
            this.huntButton.TabIndex = 8;
            this.huntButton.Text = "Hunt";
            this.huntButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.huntButton.UseVisualStyleBackColor = true;
            this.huntButton.Click += new System.EventHandler(this.huntButton_Click);
            // 
            // ViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(186)))), ((int)(((byte)(155)))));
            this.ClientSize = new System.Drawing.Size(1164, 969);
            this.Controls.Add(this.basePanel);
            this.Controls.Add(this.menu);
            this.ForeColor = System.Drawing.Color.Black;
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
            this.searchGroupBox.ResumeLayout(false);
            this.searchGroupBox.PerformLayout();
            this.operationGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private ComboBox _languageComboBox;
    private TextBox _topicsTextBox;
    private TextBox _repositoryCountTextBox;
    private ComboBox _sortDirectionComboBox;
    private Button _searchButton;
    private Button _downloadButton;
    private Button _calculateMetricsButton;
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
    private GroupBox operationGroupBox;
    private Button huntButton;
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
}
