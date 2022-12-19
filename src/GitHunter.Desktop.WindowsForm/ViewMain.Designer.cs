namespace GitHunter.Desktop;

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
            this._languageComboBox = new System.Windows.Forms.ComboBox();
            this._topicsTextBox = new System.Windows.Forms.TextBox();
            this._repositoryCountTextBox = new System.Windows.Forms.TextBox();
            this._sortDirectionComboBox = new System.Windows.Forms.ComboBox();
            this._searchButton = new System.Windows.Forms.Button();
            this._downloadMetricsButton = new System.Windows.Forms.Button();
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
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginGithubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contributorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this._languageComboBox.FormattingEnabled = true;
            this._languageComboBox.Location = new System.Drawing.Point(271, 65);
            this._languageComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._languageComboBox.Name = "_languageComboBox";
            this._languageComboBox.Size = new System.Drawing.Size(114, 28);
            this._languageComboBox.TabIndex = 0;
            // 
            // _topicsTextBox
            // 
            this._topicsTextBox.Location = new System.Drawing.Point(11, 65);
            this._topicsTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._topicsTextBox.Name = "_topicsTextBox";
            this._topicsTextBox.Size = new System.Drawing.Size(114, 27);
            this._topicsTextBox.TabIndex = 1;
            // 
            // _repositoryCountTextBox
            // 
            this._repositoryCountTextBox.Location = new System.Drawing.Point(139, 65);
            this._repositoryCountTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._repositoryCountTextBox.Name = "_repositoryCountTextBox";
            this._repositoryCountTextBox.Size = new System.Drawing.Size(114, 27);
            this._repositoryCountTextBox.TabIndex = 2;
            // 
            // _sortDirectionComboBox
            // 
            this._sortDirectionComboBox.FormattingEnabled = true;
            this._sortDirectionComboBox.Location = new System.Drawing.Point(402, 65);
            this._sortDirectionComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._sortDirectionComboBox.Name = "_sortDirectionComboBox";
            this._sortDirectionComboBox.Size = new System.Drawing.Size(114, 28);
            this._sortDirectionComboBox.TabIndex = 3;
            // 
            // _searchButton
            // 
            this._searchButton.Location = new System.Drawing.Point(535, 64);
            this._searchButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(80, 27);
            this._searchButton.TabIndex = 5;
            this._searchButton.Text = "Search";
            this._searchButton.UseVisualStyleBackColor = true;
            this._searchButton.Click += new System.EventHandler(this._searchButton_Click);
            // 
            // _downloadMetricsButton
            // 
            this._downloadMetricsButton.Location = new System.Drawing.Point(78, 35);
            this._downloadMetricsButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._downloadMetricsButton.Name = "_downloadMetricsButton";
            this._downloadMetricsButton.Size = new System.Drawing.Size(90, 27);
            this._downloadMetricsButton.TabIndex = 6;
            this._downloadMetricsButton.Text = "Download";
            this._downloadMetricsButton.UseVisualStyleBackColor = true;
            this._downloadMetricsButton.Click += new System.EventHandler(this._downloadMetricsButton_Click);
            // 
            // _calculateMetricsButton
            // 
            this._calculateMetricsButton.Location = new System.Drawing.Point(207, 35);
            this._calculateMetricsButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._calculateMetricsButton.Name = "_calculateMetricsButton";
            this._calculateMetricsButton.Size = new System.Drawing.Size(135, 27);
            this._calculateMetricsButton.TabIndex = 7;
            this._calculateMetricsButton.Text = "Calculate Metrics";
            this._calculateMetricsButton.UseVisualStyleBackColor = true;
            this._calculateMetricsButton.Click += new System.EventHandler(this._calculateMetricsButton_Click);
            // 
            // _topicsLabel
            // 
            this._topicsLabel.AutoSize = true;
            this._topicsLabel.Location = new System.Drawing.Point(11, 42);
            this._topicsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._topicsLabel.Name = "_topicsLabel";
            this._topicsLabel.Size = new System.Drawing.Size(51, 20);
            this._topicsLabel.TabIndex = 8;
            this._topicsLabel.Text = "Topics";
            // 
            // _countRepositoryLabel
            // 
            this._countRepositoryLabel.AutoSize = true;
            this._countRepositoryLabel.Location = new System.Drawing.Point(139, 40);
            this._countRepositoryLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._countRepositoryLabel.Name = "_countRepositoryLabel";
            this._countRepositoryLabel.Size = new System.Drawing.Size(103, 20);
            this._countRepositoryLabel.TabIndex = 9;
            this._countRepositoryLabel.Text = "Wanted Count";
            // 
            // _languageLabel
            // 
            this._languageLabel.AutoSize = true;
            this._languageLabel.Location = new System.Drawing.Point(271, 40);
            this._languageLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._languageLabel.Name = "_languageLabel";
            this._languageLabel.Size = new System.Drawing.Size(74, 20);
            this._languageLabel.TabIndex = 10;
            this._languageLabel.Text = "Language";
            // 
            // _sortDirectionLabel
            // 
            this._sortDirectionLabel.AutoSize = true;
            this._sortDirectionLabel.Location = new System.Drawing.Point(402, 42);
            this._sortDirectionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._sortDirectionLabel.Name = "_sortDirectionLabel";
            this._sortDirectionLabel.Size = new System.Drawing.Size(56, 20);
            this._sortDirectionLabel.TabIndex = 11;
            this._sortDirectionLabel.Text = "Sort By";
            // 
            // _repositoryDataGridView
            // 
            this._repositoryDataGridView.AllowDrop = true;
            this._repositoryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._repositoryDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this._repositoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._repositoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._repositoryDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this._repositoryDataGridView.Location = new System.Drawing.Point(2, 122);
            this._repositoryDataGridView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._repositoryDataGridView.Name = "_repositoryDataGridView";
            this._repositoryDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this._repositoryDataGridView.RowTemplate.Height = 33;
            this._repositoryDataGridView.Size = new System.Drawing.Size(651, 311);
            this._repositoryDataGridView.TabIndex = 4;
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savedRepositoriesToolStripMenuItem,
            this.loginGithubToolStripMenuItem,
            this.helpMenu});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menu.Size = new System.Drawing.Size(703, 28);
            this.menu.TabIndex = 15;
            this.menu.Text = "menuStrip1";
            // 
            // savedRepositoriesToolStripMenuItem
            // 
            this.savedRepositoriesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.savedRepositoriesToolStripMenuItem.Name = "savedRepositoriesToolStripMenuItem";
            this.savedRepositoriesToolStripMenuItem.Size = new System.Drawing.Size(94, 24);
            this.savedRepositoriesToolStripMenuItem.Text = "Repository";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this._saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this._loadToolStripMenuItem_Click);
            // 
            // loginGithubToolStripMenuItem
            // 
            this.loginGithubToolStripMenuItem.Name = "loginGithubToolStripMenuItem";
            this.loginGithubToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.loginGithubToolStripMenuItem.Text = "Login GitHub";
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contributorsToolStripMenuItem,
            this.reportToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(55, 24);
            this.helpMenu.Text = "Help";
            // 
            // contributorsToolStripMenuItem
            // 
            this.contributorsToolStripMenuItem.Name = "contributorsToolStripMenuItem";
            this.contributorsToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.contributorsToolStripMenuItem.Text = "Contributors";
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.reportToolStripMenuItem.Text = "Report";
            // 
            // basePanel
            // 
            this.basePanel.Controls.Add(this.layoutPanel);
            this.basePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basePanel.Location = new System.Drawing.Point(0, 28);
            this.basePanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.basePanel.Name = "basePanel";
            this.basePanel.Padding = new System.Windows.Forms.Padding(24, 40, 24, 24);
            this.basePanel.Size = new System.Drawing.Size(703, 579);
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
            this.layoutPanel.Location = new System.Drawing.Point(24, 40);
            this.layoutPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 3;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.layoutPanel.Size = new System.Drawing.Size(655, 515);
            this.layoutPanel.TabIndex = 5;
            // 
            // searchRepositoryPanel
            // 
            this.searchRepositoryPanel.Controls.Add(this.searchGroupBox);
            this.searchRepositoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchRepositoryPanel.Location = new System.Drawing.Point(2, 2);
            this.searchRepositoryPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.searchRepositoryPanel.Name = "searchRepositoryPanel";
            this.searchRepositoryPanel.Size = new System.Drawing.Size(651, 116);
            this.searchRepositoryPanel.TabIndex = 5;
            // 
            // searchGroupBox
            // 
            this.searchGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.searchGroupBox.Controls.Add(this._repositoryCountTextBox);
            this.searchGroupBox.Controls.Add(this._sortDirectionLabel);
            this.searchGroupBox.Controls.Add(this._searchButton);
            this.searchGroupBox.Controls.Add(this._languageLabel);
            this.searchGroupBox.Controls.Add(this._sortDirectionComboBox);
            this.searchGroupBox.Controls.Add(this._countRepositoryLabel);
            this.searchGroupBox.Controls.Add(this._languageComboBox);
            this.searchGroupBox.Controls.Add(this._topicsTextBox);
            this.searchGroupBox.Controls.Add(this._topicsLabel);
            this.searchGroupBox.Location = new System.Drawing.Point(13, 13);
            this.searchGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.searchGroupBox.Name = "searchGroupBox";
            this.searchGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.searchGroupBox.Size = new System.Drawing.Size(625, 101);
            this.searchGroupBox.TabIndex = 12;
            this.searchGroupBox.TabStop = false;
            this.searchGroupBox.Text = "Search Repositories";
            // 
            // operationGroupBox
            // 
            this.operationGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.operationGroupBox.Controls.Add(this.huntButton);
            this.operationGroupBox.Controls.Add(this._downloadMetricsButton);
            this.operationGroupBox.Controls.Add(this._calculateMetricsButton);
            this.operationGroupBox.Location = new System.Drawing.Point(47, 437);
            this.operationGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.operationGroupBox.Name = "operationGroupBox";
            this.operationGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.operationGroupBox.Size = new System.Drawing.Size(561, 75);
            this.operationGroupBox.TabIndex = 6;
            this.operationGroupBox.TabStop = false;
            this.operationGroupBox.Text = "Operations";
            // 
            // huntButton
            // 
            this.huntButton.Location = new System.Drawing.Point(387, 35);
            this.huntButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.huntButton.Name = "huntButton";
            this.huntButton.Size = new System.Drawing.Size(97, 27);
            this.huntButton.TabIndex = 8;
            this.huntButton.Text = "Hunt";
            this.huntButton.UseVisualStyleBackColor = true;
            // 
            // ViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 607);
            this.Controls.Add(this.basePanel);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(721, 654);
            this.Name = "ViewMain";
            this.Text = "Form1";
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
    private Button _downloadMetricsButton;
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
    private ToolStripMenuItem loadToolStripMenuItem;
}