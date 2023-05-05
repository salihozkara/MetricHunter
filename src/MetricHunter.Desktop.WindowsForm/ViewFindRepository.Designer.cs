namespace MetricHunter.Desktop
{
    partial class ViewFindRepository
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
            this.label1 = new System.Windows.Forms.Label();
            this._searchButton = new System.Windows.Forms.Button();
            this._topicsTextBox = new System.Windows.Forms.TextBox();
            this._repositoryNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(89, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 65);
            this.label1.TabIndex = 31;
            this.label1.Text = "Find Repository";
            // 
            // _searchButton
            // 
            this._searchButton.ImageKey = "search.png";
            this._searchButton.Location = new System.Drawing.Point(437, 347);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(128, 41);
            this._searchButton.TabIndex = 26;
            this._searchButton.Text = "Find";
            this._searchButton.UseVisualStyleBackColor = true;
            // 
            // _topicsTextBox
            // 
            this._topicsTextBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._topicsTextBox.Location = new System.Drawing.Point(89, 280);
            this._topicsTextBox.Name = "_topicsTextBox";
            this._topicsTextBox.Size = new System.Drawing.Size(476, 41);
            this._topicsTextBox.TabIndex = 23;
            // 
            // _repositoryNameLabel
            // 
            this._repositoryNameLabel.AutoSize = true;
            this._repositoryNameLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._repositoryNameLabel.Location = new System.Drawing.Point(89, 224);
            this._repositoryNameLabel.Name = "_repositoryNameLabel";
            this._repositoryNameLabel.Size = new System.Drawing.Size(216, 34);
            this._repositoryNameLabel.TabIndex = 27;
            this._repositoryNameLabel.Text = "Repository Name or URL";
            // 
            // ViewFindRepository
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 431);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._searchButton);
            this.Controls.Add(this._topicsTextBox);
            this.Controls.Add(this._repositoryNameLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewFindRepository";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Repository";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button _searchButton;
        private TextBox _topicsTextBox;
        private Label _repositoryNameLabel;
    }
}