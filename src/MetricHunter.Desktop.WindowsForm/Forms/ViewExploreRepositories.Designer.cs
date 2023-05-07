namespace MetricHunter.Desktop.Forms
{
    partial class ViewExploreRepositories
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._repositoryCountTextBox = new System.Windows.Forms.TextBox();
            this._sortDirectionLabel = new System.Windows.Forms.Label();
            this._searchButton = new System.Windows.Forms.Button();
            this._languageLabel = new System.Windows.Forms.Label();
            this._sortDirectionComboBox = new System.Windows.Forms.ComboBox();
            this._countRepositoryLabel = new System.Windows.Forms.Label();
            this._languageComboBox = new System.Windows.Forms.ComboBox();
            this._topicsTextBox = new System.Windows.Forms.TextBox();
            this._topicsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _repositoryCountTextBox
            // 
            this._repositoryCountTextBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._repositoryCountTextBox.Location = new System.Drawing.Point(228, 253);
            this._repositoryCountTextBox.Name = "_repositoryCountTextBox";
            this._repositoryCountTextBox.Size = new System.Drawing.Size(322, 41);
            this._repositoryCountTextBox.TabIndex = 13;
            // 
            // _sortDirectionLabel
            // 
            this._sortDirectionLabel.AutoSize = true;
            this._sortDirectionLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._sortDirectionLabel.Location = new System.Drawing.Point(76, 412);
            this._sortDirectionLabel.Name = "_sortDirectionLabel";
            this._sortDirectionLabel.Size = new System.Drawing.Size(123, 34);
            this._sortDirectionLabel.TabIndex = 20;
            this._sortDirectionLabel.Text = "Sort By Stars";
            // 
            // _searchButton
            // 
            this._searchButton.ImageKey = "search.png";
            this._searchButton.Location = new System.Drawing.Point(422, 484);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(128, 41);
            this._searchButton.TabIndex = 16;
            this._searchButton.Text = "Explore";
            this._searchButton.UseVisualStyleBackColor = true;
            this._searchButton.Click += new System.EventHandler(this._searchButton_Click);
            // 
            // _languageLabel
            // 
            this._languageLabel.AutoSize = true;
            this._languageLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._languageLabel.Location = new System.Drawing.Point(76, 338);
            this._languageLabel.Name = "_languageLabel";
            this._languageLabel.Size = new System.Drawing.Size(92, 34);
            this._languageLabel.TabIndex = 19;
            this._languageLabel.Text = "Language";
            // 
            // _sortDirectionComboBox
            // 
            this._sortDirectionComboBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._sortDirectionComboBox.FormattingEnabled = true;
            this._sortDirectionComboBox.Location = new System.Drawing.Point(228, 404);
            this._sortDirectionComboBox.Name = "_sortDirectionComboBox";
            this._sortDirectionComboBox.Size = new System.Drawing.Size(322, 42);
            this._sortDirectionComboBox.TabIndex = 15;
            // 
            // _countRepositoryLabel
            // 
            this._countRepositoryLabel.AutoSize = true;
            this._countRepositoryLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._countRepositoryLabel.Location = new System.Drawing.Point(76, 260);
            this._countRepositoryLabel.Name = "_countRepositoryLabel";
            this._countRepositoryLabel.Size = new System.Drawing.Size(63, 34);
            this._countRepositoryLabel.TabIndex = 18;
            this._countRepositoryLabel.Text = "Count";
            // 
            // _languageComboBox
            // 
            this._languageComboBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._languageComboBox.FormattingEnabled = true;
            this._languageComboBox.Location = new System.Drawing.Point(228, 330);
            this._languageComboBox.Name = "_languageComboBox";
            this._languageComboBox.Size = new System.Drawing.Size(322, 42);
            this._languageComboBox.TabIndex = 14;
            // 
            // _topicsTextBox
            // 
            this._topicsTextBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._topicsTextBox.Location = new System.Drawing.Point(228, 174);
            this._topicsTextBox.Name = "_topicsTextBox";
            this._topicsTextBox.Size = new System.Drawing.Size(322, 41);
            this._topicsTextBox.TabIndex = 12;
            // 
            // _topicsLabel
            // 
            this._topicsLabel.AutoSize = true;
            this._topicsLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._topicsLabel.Location = new System.Drawing.Point(76, 181);
            this._topicsLabel.Name = "_topicsLabel";
            this._topicsLabel.Size = new System.Drawing.Size(68, 34);
            this._topicsLabel.TabIndex = 17;
            this._topicsLabel.Text = "Topics";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(76, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(456, 65);
            this.label1.TabIndex = 21;
            this.label1.Text = "Explore Repositories";
            // 
            // ViewExploreRepositories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 581);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._repositoryCountTextBox);
            this.Controls.Add(this._sortDirectionLabel);
            this.Controls.Add(this._searchButton);
            this.Controls.Add(this._languageLabel);
            this.Controls.Add(this._sortDirectionComboBox);
            this.Controls.Add(this._countRepositoryLabel);
            this.Controls.Add(this._languageComboBox);
            this.Controls.Add(this._topicsTextBox);
            this.Controls.Add(this._topicsLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewExploreRepositories";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Explore Repositories";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox _repositoryCountTextBox;
        private Label _sortDirectionLabel;
        private Button _searchButton;
        private Label _languageLabel;
        private ComboBox _sortDirectionComboBox;
        private Label _countRepositoryLabel;
        private ComboBox _languageComboBox;
        private TextBox _topicsTextBox;
        private Label _topicsLabel;
        private Label label1;
    }
}