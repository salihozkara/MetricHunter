namespace MetricHunter.Desktop.Forms {
    partial class ViewFindRepository {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            label1 = new Label();
            _repositoryNameTextBox = new TextBox();
            _repositoryNameLabel = new Label();
            _findButton = new Button();
            _repositoryUrl = new LinkLabel();
            _repositoryName = new Label();
            _repositoryImage = new PictureBox();
            _repositoryPanel = new Panel();
            label6 = new Label();
            label7 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            _repositoryDescription = new Label();
            _repositoryOwner = new Label();
            _releasesButton = new Button();
            _commitsButton = new Button();
            ((System.ComponentModel.ISupportInitialize)_repositoryImage).BeginInit();
            _repositoryPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(18, 16);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(241, 45);
            label1.TabIndex = 31;
            label1.Text = "Find Repository";
            // 
            // _repositoryNameTextBox
            // 
            _repositoryNameTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            _repositoryNameTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _repositoryNameTextBox.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
            _repositoryNameTextBox.Location = new Point(26, 97);
            _repositoryNameTextBox.Margin = new Padding(2);
            _repositoryNameTextBox.Name = "_repositoryNameTextBox";
            _repositoryNameTextBox.Size = new Size(481, 30);
            _repositoryNameTextBox.TabIndex = 23;
            _repositoryNameTextBox.KeyDown += _repositoryNameTextBox_KeyDown;
            // 
            // _repositoryNameLabel
            // 
            _repositoryNameLabel.AutoSize = true;
            _repositoryNameLabel.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
            _repositoryNameLabel.Location = new Point(24, 74);
            _repositoryNameLabel.Margin = new Padding(2, 0, 2, 0);
            _repositoryNameLabel.Name = "_repositoryNameLabel";
            _repositoryNameLabel.Size = new Size(149, 24);
            _repositoryNameLabel.TabIndex = 27;
            _repositoryNameLabel.Text = "Repository Name or URL";
            // 
            // _findButton
            // 
            _findButton.ImageKey = "search.png";
            _findButton.Location = new Point(517, 97);
            _findButton.Margin = new Padding(2);
            _findButton.Name = "_findButton";
            _findButton.Size = new Size(69, 25);
            _findButton.TabIndex = 36;
            _findButton.Text = "Find";
            _findButton.UseVisualStyleBackColor = true;
            _findButton.Click += _findButton_Click;
            // 
            // _repositoryUrl
            // 
            _repositoryUrl.AutoSize = true;
            _repositoryUrl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            _repositoryUrl.Location = new Point(279, 56);
            _repositoryUrl.Margin = new Padding(2, 0, 2, 0);
            _repositoryUrl.Name = "_repositoryUrl";
            _repositoryUrl.Size = new Size(70, 19);
            _repositoryUrl.TabIndex = 43;
            _repositoryUrl.TabStop = true;
            _repositoryUrl.Text = "linkLabel1";
            // 
            // _repositoryName
            // 
            _repositoryName.AutoSize = true;
            _repositoryName.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            _repositoryName.Location = new Point(76, 48);
            _repositoryName.Margin = new Padding(2, 0, 2, 0);
            _repositoryName.Name = "_repositoryName";
            _repositoryName.Size = new Size(71, 30);
            _repositoryName.TabIndex = 41;
            _repositoryName.Text = "label1";
            // 
            // _repositoryImage
            // 
            _repositoryImage.BackColor = Color.Transparent;
            _repositoryImage.Location = new Point(0, 14);
            _repositoryImage.Margin = new Padding(2);
            _repositoryImage.Name = "_repositoryImage";
            _repositoryImage.Size = new Size(70, 60);
            _repositoryImage.SizeMode = PictureBoxSizeMode.StretchImage;
            _repositoryImage.TabIndex = 40;
            _repositoryImage.TabStop = false;
            // 
            // _repositoryPanel
            // 
            _repositoryPanel.Controls.Add(label6);
            _repositoryPanel.Controls.Add(label7);
            _repositoryPanel.Controls.Add(label5);
            _repositoryPanel.Controls.Add(label4);
            _repositoryPanel.Controls.Add(label3);
            _repositoryPanel.Controls.Add(label2);
            _repositoryPanel.Controls.Add(_repositoryDescription);
            _repositoryPanel.Controls.Add(_repositoryOwner);
            _repositoryPanel.Controls.Add(_repositoryImage);
            _repositoryPanel.Controls.Add(_repositoryUrl);
            _repositoryPanel.Controls.Add(_repositoryName);
            _repositoryPanel.Location = new Point(26, 141);
            _repositoryPanel.Margin = new Padding(2);
            _repositoryPanel.Name = "_repositoryPanel";
            _repositoryPanel.Size = new Size(560, 211);
            _repositoryPanel.TabIndex = 44;
            _repositoryPanel.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(342, 7);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(42, 15);
            label6.TabIndex = 51;
            label6.Text = "29739";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(279, 7);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(59, 15);
            label7.TabIndex = 50;
            label7.Text = "Commits:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(446, 7);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(21, 15);
            label5.TabIndex = 49;
            label5.Text = "3k";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(413, 7);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(35, 15);
            label4.TabIndex = 48;
            label4.Text = "Fork:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(515, 7);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 47;
            label3.Text = "9.9k";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(482, 7);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 46;
            label2.Text = "Star:";
            // 
            // _repositoryDescription
            // 
            _repositoryDescription.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            _repositoryDescription.Location = new Point(0, 94);
            _repositoryDescription.Margin = new Padding(2, 0, 2, 0);
            _repositoryDescription.Name = "_repositoryDescription";
            _repositoryDescription.Size = new Size(541, 118);
            _repositoryDescription.TabIndex = 45;
            _repositoryDescription.Text = "_repositoryDescription";
            // 
            // _repositoryOwner
            // 
            _repositoryOwner.AutoSize = true;
            _repositoryOwner.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            _repositoryOwner.Location = new Point(80, 30);
            _repositoryOwner.Margin = new Padding(2, 0, 2, 0);
            _repositoryOwner.Name = "_repositoryOwner";
            _repositoryOwner.Size = new Size(50, 20);
            _repositoryOwner.TabIndex = 44;
            _repositoryOwner.Text = "label1";
            // 
            // _releasesButton
            // 
            _releasesButton.ImageKey = "search.png";
            _releasesButton.Location = new Point(502, 376);
            _releasesButton.Margin = new Padding(2);
            _releasesButton.Name = "_releasesButton";
            _releasesButton.Size = new Size(84, 25);
            _releasesButton.TabIndex = 45;
            _releasesButton.Text = "Releases";
            _releasesButton.UseVisualStyleBackColor = true;
            _releasesButton.Click += _releasesButton_Click;
            // 
            // _commitsButton
            // 
            _commitsButton.ImageKey = "search.png";
            _commitsButton.Location = new Point(404, 376);
            _commitsButton.Margin = new Padding(2);
            _commitsButton.Name = "_commitsButton";
            _commitsButton.Size = new Size(90, 25);
            _commitsButton.TabIndex = 46;
            _commitsButton.Text = "Commits";
            _commitsButton.UseVisualStyleBackColor = true;
            _commitsButton.Click += _commitsButton_Click;
            // 
            // ViewFindRepository
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(606, 416);
            Controls.Add(_commitsButton);
            Controls.Add(_releasesButton);
            Controls.Add(_repositoryPanel);
            Controls.Add(_findButton);
            Controls.Add(label1);
            Controls.Add(_repositoryNameTextBox);
            Controls.Add(_repositoryNameLabel);
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ViewFindRepository";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Find Repository";
            ((System.ComponentModel.ISupportInitialize)_repositoryImage).EndInit();
            _repositoryPanel.ResumeLayout(false);
            _repositoryPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
    }
}