namespace MetricHunter.Desktop.Forms {
    partial class ViewGithubLogin {
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
            _githubToken = new TextBox();
            GithubLoginLabel = new Label();
            AuthenticateButton = new Button();
            label1 = new Label();
            githubTokenHelp = new LinkLabel();
            SuspendLayout();
            // 
            // _githubToken
            // 
            _githubToken.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
            _githubToken.Location = new Point(124, 137);
            _githubToken.Margin = new Padding(2, 2, 2, 2);
            _githubToken.Name = "_githubToken";
            _githubToken.Size = new Size(291, 30);
            _githubToken.TabIndex = 0;
            // 
            // GithubLoginLabel
            // 
            GithubLoginLabel.AutoSize = true;
            GithubLoginLabel.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
            GithubLoginLabel.Location = new Point(26, 137);
            GithubLoginLabel.Margin = new Padding(2, 0, 2, 0);
            GithubLoginLabel.Name = "GithubLoginLabel";
            GithubLoginLabel.Size = new Size(85, 24);
            GithubLoginLabel.TabIndex = 1;
            GithubLoginLabel.Text = "Github Token";
            // 
            // AuthenticateButton
            // 
            AuthenticateButton.Font = new Font("Dubai", 10F, FontStyle.Regular, GraphicsUnit.Point);
            AuthenticateButton.Location = new Point(314, 176);
            AuthenticateButton.Margin = new Padding(2, 2, 2, 2);
            AuthenticateButton.Name = "AuthenticateButton";
            AuthenticateButton.Size = new Size(100, 24);
            AuthenticateButton.TabIndex = 2;
            AuthenticateButton.Text = "Authenticate";
            AuthenticateButton.UseVisualStyleBackColor = true;
            AuthenticateButton.Click += AuthenticateButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(113, 42);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(204, 45);
            label1.TabIndex = 3;
            label1.Text = "Github Login";
            // 
            // githubTokenHelp
            // 
            githubTokenHelp.AutoSize = true;
            githubTokenHelp.Location = new Point(24, 215);
            githubTokenHelp.Margin = new Padding(2, 0, 2, 0);
            githubTokenHelp.Name = "githubTokenHelp";
            githubTokenHelp.Size = new Size(170, 15);
            githubTokenHelp.TabIndex = 4;
            githubTokenHelp.TabStop = true;
            githubTokenHelp.Text = "How to retrieve a Github Token";
            githubTokenHelp.LinkClicked += githubTokenHelp_LinkClicked;
            // 
            // ViewGithubLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 248);
            Controls.Add(githubTokenHelp);
            Controls.Add(label1);
            Controls.Add(AuthenticateButton);
            Controls.Add(GithubLoginLabel);
            Controls.Add(_githubToken);
            Margin = new Padding(2, 2, 2, 2);
            Name = "ViewGithubLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ViewGithubLogin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox _githubToken;
        private Label GithubLoginLabel;
        private Button AuthenticateButton;
        private Label label1;
        private LinkLabel githubTokenHelp;
    }
}