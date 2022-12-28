using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitHunter.Desktop.Presenters;
using GitHunter.Desktop.Views;

namespace GitHunter.Desktop
{
    public partial class ViewGithubLogin : Form, IViewGithubLogin
    {
        public IViewGithubLoginPresenter Presenter { get; set; }

        public ViewGithubLogin()
        {
            InitializeComponent();
        }

        private void AuthenticateButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.GithubToken = _githubToken.Text;
            Properties.Settings.Default.Save();
            
            Close();
        }

        public void Run()
        {
            ShowDialog();
        }
    }
}
