namespace MetricHunter.Desktop.Forms
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
            this._repositoryNameTextBox = new System.Windows.Forms.TextBox();
            this._repositoryNameLabel = new System.Windows.Forms.Label();
            this._findButton = new System.Windows.Forms.Button();
            this._repositoryUrl = new System.Windows.Forms.LinkLabel();
            this._repositoryName = new System.Windows.Forms.Label();
            this._repositoryImage = new System.Windows.Forms.PictureBox();
            this._repositoryPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._repositoryDescription = new System.Windows.Forms.Label();
            this._repositoryOwner = new System.Windows.Forms.Label();
            this._releasesButton = new System.Windows.Forms.Button();
            this._commitsButton = new System.Windows.Forms.Button();
            this._downloadButton = new System.Windows.Forms.Button();
            this._huntButton = new System.Windows.Forms.Button();
            this._calculateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._repositoryImage)).BeginInit();
            this._repositoryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(26, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 65);
            this.label1.TabIndex = 31;
            this.label1.Text = "Find Repository";
            // 
            // _repositoryNameTextBox
            // 
            this._repositoryNameTextBox.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._repositoryNameTextBox.Location = new System.Drawing.Point(37, 162);
            this._repositoryNameTextBox.Name = "_repositoryNameTextBox";
            this._repositoryNameTextBox.Size = new System.Drawing.Size(685, 41);
            this._repositoryNameTextBox.TabIndex = 23;
            // 
            // _repositoryNameLabel
            // 
            this._repositoryNameLabel.AutoSize = true;
            this._repositoryNameLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._repositoryNameLabel.Location = new System.Drawing.Point(35, 124);
            this._repositoryNameLabel.Name = "_repositoryNameLabel";
            this._repositoryNameLabel.Size = new System.Drawing.Size(216, 34);
            this._repositoryNameLabel.TabIndex = 27;
            this._repositoryNameLabel.Text = "Repository Name or URL";
            // 
            // _findButton
            // 
            this._findButton.ImageKey = "search.png";
            this._findButton.Location = new System.Drawing.Point(738, 162);
            this._findButton.Name = "_findButton";
            this._findButton.Size = new System.Drawing.Size(99, 41);
            this._findButton.TabIndex = 36;
            this._findButton.Text = "Find";
            this._findButton.UseVisualStyleBackColor = true;
            this._findButton.Click += new System.EventHandler(this._findButton_Click);
            // 
            // _repositoryUrl
            // 
            this._repositoryUrl.AutoSize = true;
            this._repositoryUrl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._repositoryUrl.Location = new System.Drawing.Point(399, 93);
            this._repositoryUrl.Name = "_repositoryUrl";
            this._repositoryUrl.Size = new System.Drawing.Size(100, 28);
            this._repositoryUrl.TabIndex = 43;
            this._repositoryUrl.TabStop = true;
            this._repositoryUrl.Text = "linkLabel1";
            // 
            // _repositoryName
            // 
            this._repositoryName.AutoSize = true;
            this._repositoryName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._repositoryName.Location = new System.Drawing.Point(109, 80);
            this._repositoryName.Name = "_repositoryName";
            this._repositoryName.Size = new System.Drawing.Size(105, 45);
            this._repositoryName.TabIndex = 41;
            this._repositoryName.Text = "label1";
            // 
            // _repositoryImage
            // 
            this._repositoryImage.BackColor = System.Drawing.Color.Transparent;
            this._repositoryImage.Location = new System.Drawing.Point(0, 23);
            this._repositoryImage.Name = "_repositoryImage";
            this._repositoryImage.Size = new System.Drawing.Size(100, 100);
            this._repositoryImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._repositoryImage.TabIndex = 40;
            this._repositoryImage.TabStop = false;
            // 
            // _repositoryPanel
            // 
            this._repositoryPanel.Controls.Add(this.label6);
            this._repositoryPanel.Controls.Add(this.label7);
            this._repositoryPanel.Controls.Add(this.label5);
            this._repositoryPanel.Controls.Add(this.label4);
            this._repositoryPanel.Controls.Add(this.label3);
            this._repositoryPanel.Controls.Add(this.label2);
            this._repositoryPanel.Controls.Add(this._repositoryDescription);
            this._repositoryPanel.Controls.Add(this._repositoryOwner);
            this._repositoryPanel.Controls.Add(this._repositoryImage);
            this._repositoryPanel.Controls.Add(this._repositoryUrl);
            this._repositoryPanel.Controls.Add(this._repositoryName);
            this._repositoryPanel.Location = new System.Drawing.Point(37, 235);
            this._repositoryPanel.Name = "_repositoryPanel";
            this._repositoryPanel.Size = new System.Drawing.Size(800, 352);
            this._repositoryPanel.TabIndex = 44;
            this._repositoryPanel.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(488, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 25);
            this.label6.TabIndex = 51;
            this.label6.Text = "29739";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(399, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 25);
            this.label7.TabIndex = 50;
            this.label7.Text = "Commits:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(637, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 25);
            this.label5.TabIndex = 49;
            this.label5.Text = "3k";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(590, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 25);
            this.label4.TabIndex = 48;
            this.label4.Text = "Fork:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(736, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 25);
            this.label3.TabIndex = 47;
            this.label3.Text = "9.9k";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(689, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 25);
            this.label2.TabIndex = 46;
            this.label2.Text = "Star:";
            // 
            // _repositoryDescription
            // 
            this._repositoryDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._repositoryDescription.Location = new System.Drawing.Point(0, 156);
            this._repositoryDescription.Name = "_repositoryDescription";
            this._repositoryDescription.Size = new System.Drawing.Size(773, 196);
            this._repositoryDescription.TabIndex = 45;
            this._repositoryDescription.Text = "_repositoryDescription";
            // 
            // _repositoryOwner
            // 
            this._repositoryOwner.AutoSize = true;
            this._repositoryOwner.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._repositoryOwner.Location = new System.Drawing.Point(114, 50);
            this._repositoryOwner.Name = "_repositoryOwner";
            this._repositoryOwner.Size = new System.Drawing.Size(71, 30);
            this._repositoryOwner.TabIndex = 44;
            this._repositoryOwner.Text = "label1";
            // 
            // _releasesButton
            // 
            this._releasesButton.ImageKey = "search.png";
            this._releasesButton.Location = new System.Drawing.Point(719, 608);
            this._releasesButton.Name = "_releasesButton";
            this._releasesButton.Size = new System.Drawing.Size(120, 41);
            this._releasesButton.TabIndex = 45;
            this._releasesButton.Text = "Releases";
            this._releasesButton.UseVisualStyleBackColor = true;
            this._releasesButton.Click += new System.EventHandler(this._releasesButton_Click);
            // 
            // _commitsButton
            // 
            this._commitsButton.ImageKey = "search.png";
            this._commitsButton.Location = new System.Drawing.Point(584, 608);
            this._commitsButton.Name = "_commitsButton";
            this._commitsButton.Size = new System.Drawing.Size(129, 41);
            this._commitsButton.TabIndex = 46;
            this._commitsButton.Text = "Commits";
            this._commitsButton.UseVisualStyleBackColor = true;
            this._commitsButton.Click += new System.EventHandler(this._commitsButton_Click);
            // 
            // _downloadButton
            // 
            this._downloadButton.ImageKey = "search.png";
            this._downloadButton.Location = new System.Drawing.Point(39, 608);
            this._downloadButton.Name = "_downloadButton";
            this._downloadButton.Size = new System.Drawing.Size(140, 41);
            this._downloadButton.TabIndex = 47;
            this._downloadButton.Text = "Download";
            this._downloadButton.UseVisualStyleBackColor = true;
            this._downloadButton.Click += new System.EventHandler(this._downloadButton_Click);
            // 
            // _huntButton
            // 
            this._huntButton.ImageKey = "search.png";
            this._huntButton.Location = new System.Drawing.Point(379, 608);
            this._huntButton.Name = "_huntButton";
            this._huntButton.Size = new System.Drawing.Size(95, 41);
            this._huntButton.TabIndex = 48;
            this._huntButton.Text = "Hunt";
            this._huntButton.UseVisualStyleBackColor = true;
            this._huntButton.Click += new System.EventHandler(this._huntButton_Click);
            // 
            // _calculateButton
            // 
            this._calculateButton.ImageKey = "search.png";
            this._calculateButton.Location = new System.Drawing.Point(185, 608);
            this._calculateButton.Name = "_calculateButton";
            this._calculateButton.Size = new System.Drawing.Size(188, 41);
            this._calculateButton.TabIndex = 49;
            this._calculateButton.Text = "Calculate Metrics";
            this._calculateButton.UseVisualStyleBackColor = true;
            this._calculateButton.Click += new System.EventHandler(this._calculateButton_Click);
            // 
            // ViewFindRepository
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 229);
            this.Controls.Add(this._calculateButton);
            this.Controls.Add(this._huntButton);
            this.Controls.Add(this._downloadButton);
            this.Controls.Add(this._commitsButton);
            this.Controls.Add(this._releasesButton);
            this.Controls.Add(this._repositoryPanel);
            this.Controls.Add(this._findButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._repositoryNameTextBox);
            this.Controls.Add(this._repositoryNameLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewFindRepository";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Repository";
            ((System.ComponentModel.ISupportInitialize)(this._repositoryImage)).EndInit();
            this._repositoryPanel.ResumeLayout(false);
            this._repositoryPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox _repositoryNameTextBox;
        private Label _repositoryNameLabel;
        private Button _findButton;
        private LinkLabel _repositoryUrl;
        private Label _repositoryName;
        private PictureBox _repositoryImage;
        private Panel _repositoryPanel;
        private Label _repositoryOwner;
        private Label _repositoryDescription;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label6;
        private Label label7;
        private Button _releasesButton;
        private Button _commitsButton;
        private Button _downloadButton;
        private Button _huntButton;
        private Button _calculateButton;
    }
}