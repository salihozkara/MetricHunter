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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.denemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aaaaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._repositoryDataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _languageComboBox
            // 
            this._languageComboBox.FormattingEnabled = true;
            this._languageComboBox.Location = new System.Drawing.Point(339, 78);
            this._languageComboBox.Name = "_languageComboBox";
            this._languageComboBox.Size = new System.Drawing.Size(182, 33);
            this._languageComboBox.TabIndex = 0;
            // 
            // _topicsTextBox
            // 
            this._topicsTextBox.Location = new System.Drawing.Point(6, 78);
            this._topicsTextBox.Name = "_topicsTextBox";
            this._topicsTextBox.Size = new System.Drawing.Size(150, 31);
            this._topicsTextBox.TabIndex = 1;
            // 
            // _repositoryCountTextBox
            // 
            this._repositoryCountTextBox.Location = new System.Drawing.Point(162, 78);
            this._repositoryCountTextBox.Name = "_repositoryCountTextBox";
            this._repositoryCountTextBox.Size = new System.Drawing.Size(150, 31);
            this._repositoryCountTextBox.TabIndex = 2;
            // 
            // _sortDirectionComboBox
            // 
            this._sortDirectionComboBox.FormattingEnabled = true;
            this._sortDirectionComboBox.Location = new System.Drawing.Point(545, 78);
            this._sortDirectionComboBox.Name = "_sortDirectionComboBox";
            this._sortDirectionComboBox.Size = new System.Drawing.Size(182, 33);
            this._sortDirectionComboBox.TabIndex = 3;
            // 
            // _searchButton
            // 
            this._searchButton.Location = new System.Drawing.Point(774, 78);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(112, 34);
            this._searchButton.TabIndex = 5;
            this._searchButton.Text = "Search";
            this._searchButton.UseVisualStyleBackColor = true;
            this._searchButton.Click += new System.EventHandler(this._searchButton_Click);
            // 
            // _downloadMetricsButton
            // 
            this._downloadMetricsButton.Location = new System.Drawing.Point(66, 44);
            this._downloadMetricsButton.Name = "_downloadMetricsButton";
            this._downloadMetricsButton.Size = new System.Drawing.Size(112, 34);
            this._downloadMetricsButton.TabIndex = 6;
            this._downloadMetricsButton.Text = "Download";
            this._downloadMetricsButton.UseVisualStyleBackColor = true;
            this._downloadMetricsButton.Click += new System.EventHandler(this._downloadMetricsButton_Click);
            // 
            // _calculateMetricsButton
            // 
            this._calculateMetricsButton.Location = new System.Drawing.Point(232, 44);
            this._calculateMetricsButton.Name = "_calculateMetricsButton";
            this._calculateMetricsButton.Size = new System.Drawing.Size(169, 34);
            this._calculateMetricsButton.TabIndex = 7;
            this._calculateMetricsButton.Text = "Calculate Metrics";
            this._calculateMetricsButton.UseVisualStyleBackColor = true;
            this._calculateMetricsButton.Click += new System.EventHandler(this._calculateMetricsButton_Click);
            // 
            // _topicsLabel
            // 
            this._topicsLabel.AutoSize = true;
            this._topicsLabel.Location = new System.Drawing.Point(6, 50);
            this._topicsLabel.Name = "_topicsLabel";
            this._topicsLabel.Size = new System.Drawing.Size(61, 25);
            this._topicsLabel.TabIndex = 8;
            this._topicsLabel.Text = "Topics";
            // 
            // _countRepositoryLabel
            // 
            this._countRepositoryLabel.AutoSize = true;
            this._countRepositoryLabel.Location = new System.Drawing.Point(162, 50);
            this._countRepositoryLabel.Name = "_countRepositoryLabel";
            this._countRepositoryLabel.Size = new System.Drawing.Size(126, 25);
            this._countRepositoryLabel.TabIndex = 9;
            this._countRepositoryLabel.Text = "Wanted Count";
            // 
            // _languageLabel
            // 
            this._languageLabel.AutoSize = true;
            this._languageLabel.Location = new System.Drawing.Point(339, 50);
            this._languageLabel.Name = "_languageLabel";
            this._languageLabel.Size = new System.Drawing.Size(89, 25);
            this._languageLabel.TabIndex = 10;
            this._languageLabel.Text = "Language";
            // 
            // _sortDirectionLabel
            // 
            this._sortDirectionLabel.AutoSize = true;
            this._sortDirectionLabel.Location = new System.Drawing.Point(545, 50);
            this._sortDirectionLabel.Name = "_sortDirectionLabel";
            this._sortDirectionLabel.Size = new System.Drawing.Size(69, 25);
            this._sortDirectionLabel.TabIndex = 11;
            this._sortDirectionLabel.Text = "Sort By";
            // 
            // _repositoryDataGridView
            // 
            this._repositoryDataGridView.AllowDrop = true;
            this._repositoryDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this._repositoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._repositoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._repositoryDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this._repositoryDataGridView.Location = new System.Drawing.Point(3, 153);
            this._repositoryDataGridView.Name = "_repositoryDataGridView";
            this._repositoryDataGridView.RowHeadersWidth = 62;
            this._repositoryDataGridView.RowTemplate.Height = 33;
            this._repositoryDataGridView.Size = new System.Drawing.Size(1128, 401);
            this._repositoryDataGridView.TabIndex = 4;
            this._repositoryDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._repositoryDataGridView_CellContentClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1194, 33);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.denemeToolStripMenuItem,
            this.aaaaToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(187, 29);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // denemeToolStripMenuItem
            // 
            this.denemeToolStripMenuItem.Name = "denemeToolStripMenuItem";
            this.denemeToolStripMenuItem.Size = new System.Drawing.Size(180, 34);
            this.denemeToolStripMenuItem.Text = "Deneme";
            // 
            // aaaaToolStripMenuItem
            // 
            this.aaaaToolStripMenuItem.Name = "aaaaToolStripMenuItem";
            this.aaaaToolStripMenuItem.Size = new System.Drawing.Size(180, 34);
            this.aaaaToolStripMenuItem.Text = "Aaaa";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 33);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(30);
            this.panel1.Size = new System.Drawing.Size(1194, 717);
            this.panel1.TabIndex = 16;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._repositoryDataGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(30, 30);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1134, 657);
            this.tableLayoutPanel1.TabIndex = 5;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1128, 144);
            this.panel2.TabIndex = 5;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this._repositoryCountTextBox);
            this.groupBox2.Controls.Add(this._sortDirectionLabel);
            this.groupBox2.Controls.Add(this._searchButton);
            this.groupBox2.Controls.Add(this._languageLabel);
            this.groupBox2.Controls.Add(this._sortDirectionComboBox);
            this.groupBox2.Controls.Add(this._countRepositoryLabel);
            this.groupBox2.Controls.Add(this._languageComboBox);
            this.groupBox2.Controls.Add(this._topicsTextBox);
            this.groupBox2.Controls.Add(this._topicsLabel);
            this.groupBox2.Location = new System.Drawing.Point(121, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(898, 126);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Repositories";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this._downloadMetricsButton);
            this.groupBox1.Controls.Add(this._calculateMetricsButton);
            this.groupBox1.Location = new System.Drawing.Point(216, 560);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(701, 94);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operations";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(209, 34);
            this.button1.TabIndex = 8;
            this.button1.Text = "Calculate and Delete";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 750);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ViewMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this._viewMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this._repositoryDataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
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
    private MenuStrip menuStrip1;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem denemeToolStripMenuItem;
    private ToolStripMenuItem aaaaToolStripMenuItem;
    private Panel panel1;
    private TableLayoutPanel tableLayoutPanel1;
    private Panel panel2;
    private GroupBox groupBox1;
    private Button button1;
    private GroupBox groupBox2;
}