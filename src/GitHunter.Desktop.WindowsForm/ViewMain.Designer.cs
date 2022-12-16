﻿namespace GitHunter.Desktop;

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
            this._jsonPathTextBox = new System.Windows.Forms.TextBox();
            this._jsonPathSelectButton = new System.Windows.Forms.Button();
            this._jsonPathLabel = new System.Windows.Forms.Label();
            this._repositoryDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this._repositoryDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _languageComboBox
            // 
            this._languageComboBox.FormattingEnabled = true;
            this._languageComboBox.Location = new System.Drawing.Point(528, 141);
            this._languageComboBox.Name = "_languageComboBox";
            this._languageComboBox.Size = new System.Drawing.Size(182, 33);
            this._languageComboBox.TabIndex = 0;
            // 
            // _topicsTextBox
            // 
            this._topicsTextBox.Location = new System.Drawing.Point(178, 141);
            this._topicsTextBox.Name = "_topicsTextBox";
            this._topicsTextBox.Size = new System.Drawing.Size(150, 31);
            this._topicsTextBox.TabIndex = 1;
            // 
            // _repositoryCountTextBox
            // 
            this._repositoryCountTextBox.Location = new System.Drawing.Point(351, 141);
            this._repositoryCountTextBox.Name = "_repositoryCountTextBox";
            this._repositoryCountTextBox.Size = new System.Drawing.Size(150, 31);
            this._repositoryCountTextBox.TabIndex = 2;
            // 
            // _sortDirectionComboBox
            // 
            this._sortDirectionComboBox.FormattingEnabled = true;
            this._sortDirectionComboBox.Location = new System.Drawing.Point(734, 141);
            this._sortDirectionComboBox.Name = "_sortDirectionComboBox";
            this._sortDirectionComboBox.Size = new System.Drawing.Size(182, 33);
            this._sortDirectionComboBox.TabIndex = 3;
            // 
            // _searchButton
            // 
            this._searchButton.Location = new System.Drawing.Point(258, 235);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(112, 34);
            this._searchButton.TabIndex = 5;
            this._searchButton.Text = "Ara";
            this._searchButton.UseVisualStyleBackColor = true;
            this._searchButton.Click += new System.EventHandler(this._searchButton_Click);
            // 
            // _downloadMetricsButton
            // 
            this._downloadMetricsButton.Location = new System.Drawing.Point(431, 235);
            this._downloadMetricsButton.Name = "_downloadMetricsButton";
            this._downloadMetricsButton.Size = new System.Drawing.Size(112, 34);
            this._downloadMetricsButton.TabIndex = 6;
            this._downloadMetricsButton.Text = "İndir";
            this._downloadMetricsButton.UseVisualStyleBackColor = true;
            this._downloadMetricsButton.Click += new System.EventHandler(this._downloadMetricsButton_Click);
            // 
            // _calculateMetricsButton
            // 
            this._calculateMetricsButton.Location = new System.Drawing.Point(595, 235);
            this._calculateMetricsButton.Name = "_calculateMetricsButton";
            this._calculateMetricsButton.Size = new System.Drawing.Size(234, 34);
            this._calculateMetricsButton.TabIndex = 7;
            this._calculateMetricsButton.Text = "Metrik Hesapla";
            this._calculateMetricsButton.UseVisualStyleBackColor = true;
            this._calculateMetricsButton.Click += new System.EventHandler(this._calculateMetricsButton_Click);
            // 
            // _topicsLabel
            // 
            this._topicsLabel.AutoSize = true;
            this._topicsLabel.Location = new System.Drawing.Point(178, 113);
            this._topicsLabel.Name = "_topicsLabel";
            this._topicsLabel.Size = new System.Drawing.Size(61, 25);
            this._topicsLabel.TabIndex = 8;
            this._topicsLabel.Text = "Topics";
            // 
            // _countRepositoryLabel
            // 
            this._countRepositoryLabel.AutoSize = true;
            this._countRepositoryLabel.Location = new System.Drawing.Point(351, 113);
            this._countRepositoryLabel.Name = "_countRepositoryLabel";
            this._countRepositoryLabel.Size = new System.Drawing.Size(146, 25);
            this._countRepositoryLabel.TabIndex = 9;
            this._countRepositoryLabel.Text = "Repository Sayısı";
            // 
            // _languageLabel
            // 
            this._languageLabel.AutoSize = true;
            this._languageLabel.Location = new System.Drawing.Point(528, 113);
            this._languageLabel.Name = "_languageLabel";
            this._languageLabel.Size = new System.Drawing.Size(95, 25);
            this._languageLabel.TabIndex = 10;
            this._languageLabel.Text = "Yazılım Dili";
            // 
            // _sortDirectionLabel
            // 
            this._sortDirectionLabel.AutoSize = true;
            this._sortDirectionLabel.Location = new System.Drawing.Point(734, 113);
            this._sortDirectionLabel.Name = "_sortDirectionLabel";
            this._sortDirectionLabel.Size = new System.Drawing.Size(79, 25);
            this._sortDirectionLabel.TabIndex = 11;
            this._sortDirectionLabel.Text = "Sıralama";
            // 
            // _jsonPathTextBox
            // 
            this._jsonPathTextBox.Location = new System.Drawing.Point(258, 45);
            this._jsonPathTextBox.Name = "_jsonPathTextBox";
            this._jsonPathTextBox.Size = new System.Drawing.Size(571, 31);
            this._jsonPathTextBox.TabIndex = 12;
            // 
            // _jsonPathSelectButton
            // 
            this._jsonPathSelectButton.Location = new System.Drawing.Point(835, 45);
            this._jsonPathSelectButton.Name = "_jsonPathSelectButton";
            this._jsonPathSelectButton.Size = new System.Drawing.Size(42, 34);
            this._jsonPathSelectButton.TabIndex = 13;
            this._jsonPathSelectButton.Text = "...";
            this._jsonPathSelectButton.UseVisualStyleBackColor = true;
            this._jsonPathSelectButton.Click += new System.EventHandler(this._jsonPathSelectButton_Click);
            // 
            // _jsonPathLabel
            // 
            this._jsonPathLabel.AutoSize = true;
            this._jsonPathLabel.Location = new System.Drawing.Point(153, 48);
            this._jsonPathLabel.Name = "_jsonPathLabel";
            this._jsonPathLabel.Size = new System.Drawing.Size(86, 25);
            this._jsonPathLabel.TabIndex = 14;
            this._jsonPathLabel.Text = "Json Path";
            // 
            // _repositoryDataGridView
            // 
            this._repositoryDataGridView.AllowDrop = true;
            this._repositoryDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this._repositoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._repositoryDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this._repositoryDataGridView.Location = new System.Drawing.Point(119, 295);
            this._repositoryDataGridView.Name = "_repositoryDataGridView";
            this._repositoryDataGridView.RowHeadersWidth = 62;
            this._repositoryDataGridView.RowTemplate.Height = 33;
            this._repositoryDataGridView.Size = new System.Drawing.Size(797, 371);
            this._repositoryDataGridView.TabIndex = 4;
            // 
            // ViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 695);
            this.Controls.Add(this._jsonPathLabel);
            this.Controls.Add(this._jsonPathSelectButton);
            this.Controls.Add(this._jsonPathTextBox);
            this.Controls.Add(this._sortDirectionLabel);
            this.Controls.Add(this._languageLabel);
            this.Controls.Add(this._countRepositoryLabel);
            this.Controls.Add(this._topicsLabel);
            this.Controls.Add(this._calculateMetricsButton);
            this.Controls.Add(this._downloadMetricsButton);
            this.Controls.Add(this._searchButton);
            this.Controls.Add(this._repositoryDataGridView);
            this.Controls.Add(this._sortDirectionComboBox);
            this.Controls.Add(this._repositoryCountTextBox);
            this.Controls.Add(this._topicsTextBox);
            this.Controls.Add(this._languageComboBox);
            this.Name = "ViewMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this._viewMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this._repositoryDataGridView)).EndInit();
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
    private TextBox _jsonPathTextBox;
    private Button _jsonPathSelectButton;
    private Label _jsonPathLabel;
    private DataGridView _repositoryDataGridView;
}