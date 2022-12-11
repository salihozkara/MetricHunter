namespace GitHunter.Desktop;

partial class MainForm
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
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.topicTextBox = new System.Windows.Forms.TextBox();
            this.repositoryCountTextBox = new System.Windows.Forms.TextBox();
            this.orderTypeComboBox = new System.Windows.Forms.ComboBox();
            this.repositoryDataGrid = new System.Windows.Forms.DataGridView();
            this.searchButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.calculateMetricButton = new System.Windows.Forms.Button();
            this.topicsLabel = new System.Windows.Forms.Label();
            this.countRepositoryLabel = new System.Windows.Forms.Label();
            this.languageLabel = new System.Windows.Forms.Label();
            this.orderTypeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // languageComboBox
            // 
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Location = new System.Drawing.Point(503, 73);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(182, 33);
            this.languageComboBox.TabIndex = 0;
            // 
            // topicTextBox
            // 
            this.topicTextBox.Location = new System.Drawing.Point(153, 73);
            this.topicTextBox.Name = "topicTextBox";
            this.topicTextBox.Size = new System.Drawing.Size(150, 31);
            this.topicTextBox.TabIndex = 1;
            // 
            // repositoryCountTextBox
            // 
            this.repositoryCountTextBox.Location = new System.Drawing.Point(326, 73);
            this.repositoryCountTextBox.Name = "repositoryCountTextBox";
            this.repositoryCountTextBox.Size = new System.Drawing.Size(150, 31);
            this.repositoryCountTextBox.TabIndex = 2;
            // 
            // orderTypeComboBox
            // 
            this.orderTypeComboBox.FormattingEnabled = true;
            this.orderTypeComboBox.Location = new System.Drawing.Point(709, 73);
            this.orderTypeComboBox.Name = "orderTypeComboBox";
            this.orderTypeComboBox.Size = new System.Drawing.Size(182, 33);
            this.orderTypeComboBox.TabIndex = 3;
            // 
            // repositoryDataGrid
            // 
            this.repositoryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.repositoryDataGrid.Location = new System.Drawing.Point(94, 227);
            this.repositoryDataGrid.Name = "repositoryDataGrid";
            this.repositoryDataGrid.RowHeadersWidth = 62;
            this.repositoryDataGrid.RowTemplate.Height = 33;
            this.repositoryDataGrid.Size = new System.Drawing.Size(797, 371);
            this.repositoryDataGrid.TabIndex = 4;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(233, 167);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(112, 34);
            this.searchButton.TabIndex = 5;
            this.searchButton.Text = "Ara";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(406, 167);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(112, 34);
            this.downloadButton.TabIndex = 6;
            this.downloadButton.Text = "İndir";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // calculateMetricButton
            // 
            this.calculateMetricButton.Location = new System.Drawing.Point(570, 167);
            this.calculateMetricButton.Name = "calculateMetricButton";
            this.calculateMetricButton.Size = new System.Drawing.Size(234, 34);
            this.calculateMetricButton.TabIndex = 7;
            this.calculateMetricButton.Text = "Metrik Hesapla";
            this.calculateMetricButton.UseVisualStyleBackColor = true;
            this.calculateMetricButton.Click += new System.EventHandler(this.calculateMetricButton_Click);
            // 
            // topicsLabel
            // 
            this.topicsLabel.AutoSize = true;
            this.topicsLabel.Location = new System.Drawing.Point(153, 45);
            this.topicsLabel.Name = "topicsLabel";
            this.topicsLabel.Size = new System.Drawing.Size(61, 25);
            this.topicsLabel.TabIndex = 8;
            this.topicsLabel.Text = "Topics";
            // 
            // countRepositoryLabel
            // 
            this.countRepositoryLabel.AutoSize = true;
            this.countRepositoryLabel.Location = new System.Drawing.Point(326, 45);
            this.countRepositoryLabel.Name = "countRepositoryLabel";
            this.countRepositoryLabel.Size = new System.Drawing.Size(146, 25);
            this.countRepositoryLabel.TabIndex = 9;
            this.countRepositoryLabel.Text = "Repository Sayısı";
            // 
            // languageLabel
            // 
            this.languageLabel.AutoSize = true;
            this.languageLabel.Location = new System.Drawing.Point(503, 45);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(95, 25);
            this.languageLabel.TabIndex = 10;
            this.languageLabel.Text = "Yazılım Dili";
            // 
            // orderTypeLabel
            // 
            this.orderTypeLabel.AutoSize = true;
            this.orderTypeLabel.Location = new System.Drawing.Point(709, 45);
            this.orderTypeLabel.Name = "orderTypeLabel";
            this.orderTypeLabel.Size = new System.Drawing.Size(79, 25);
            this.orderTypeLabel.TabIndex = 11;
            this.orderTypeLabel.Text = "Sıralama";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 695);
            this.Controls.Add(this.orderTypeLabel);
            this.Controls.Add(this.languageLabel);
            this.Controls.Add(this.countRepositoryLabel);
            this.Controls.Add(this.topicsLabel);
            this.Controls.Add(this.calculateMetricButton);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.repositoryDataGrid);
            this.Controls.Add(this.orderTypeComboBox);
            this.Controls.Add(this.repositoryCountTextBox);
            this.Controls.Add(this.topicTextBox);
            this.Controls.Add(this.languageComboBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.repositoryDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private ComboBox languageComboBox;
    private TextBox topicTextBox;
    private TextBox repositoryCountTextBox;
    private ComboBox orderTypeComboBox;
    private DataGridView repositoryDataGrid;
    private Button searchButton;
    private Button downloadButton;
    private Button calculateMetricButton;
    private Label topicsLabel;
    private Label countRepositoryLabel;
    private Label languageLabel;
    private Label orderTypeLabel;
}