namespace MetricHunter.Desktop
{
    partial class ViewGithubLogin
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
            this._githubToken = new System.Windows.Forms.TextBox();
            this.GithubLoginLabel = new System.Windows.Forms.Label();
            this.AuthenticateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // _githubToken
            // 
            this._githubToken.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._githubToken.Location = new System.Drawing.Point(177, 228);
            this._githubToken.Name = "_githubToken";
            this._githubToken.Size = new System.Drawing.Size(414, 41);
            this._githubToken.TabIndex = 0;
            // 
            // GithubLoginLabel
            // 
            this.GithubLoginLabel.AutoSize = true;
            this.GithubLoginLabel.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GithubLoginLabel.Location = new System.Drawing.Point(37, 228);
            this.GithubLoginLabel.Name = "GithubLoginLabel";
            this.GithubLoginLabel.Size = new System.Drawing.Size(125, 34);
            this.GithubLoginLabel.TabIndex = 1;
            this.GithubLoginLabel.Text = "Github Token";
            // 
            // AuthenticateButton
            // 
            this.AuthenticateButton.Font = new System.Drawing.Font("Dubai", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AuthenticateButton.Location = new System.Drawing.Point(448, 294);
            this.AuthenticateButton.Name = "AuthenticateButton";
            this.AuthenticateButton.Size = new System.Drawing.Size(143, 40);
            this.AuthenticateButton.TabIndex = 2;
            this.AuthenticateButton.Text = "Authenticate";
            this.AuthenticateButton.UseVisualStyleBackColor = true;
            this.AuthenticateButton.Click += new System.EventHandler(this.AuthenticateButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(162, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 65);
            this.label1.TabIndex = 3;
            this.label1.Text = "Github Login";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // ViewGithubLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 413);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AuthenticateButton);
            this.Controls.Add(this.GithubLoginLabel);
            this.Controls.Add(this._githubToken);
            this.Name = "ViewGithubLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewGithubLogin";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox _githubToken;
        private Label GithubLoginLabel;
        private Button AuthenticateButton;
        private Label label1;
        private FileSystemWatcher fileSystemWatcher1;
    }
}