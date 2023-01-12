using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetricHunter.Desktop.Presenters;
using MetricHunter.Desktop.Views;

namespace MetricHunter.Desktop
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

        private void githubTokenHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var githubTokenHelpLink = "https://github.com/salihozkara/MetricHunter/blob/master/doc/UserGuide.md#how-to-authenticate";

            var ps = new ProcessStartInfo(githubTokenHelpLink)
            {
                UseShellExecute = true,
                Verb = "open"
            };

            Process.Start(ps);
        }
    }
}
