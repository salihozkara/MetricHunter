using System.Diagnostics;
using MetricHunter.Desktop.Presenters;
using MetricHunter.Desktop.Properties;
using MetricHunter.Desktop.Views;

namespace MetricHunter.Desktop.Forms;

public partial class ViewGithubLogin : Form, IViewGithubLogin
{
    public ViewGithubLogin()
    {
        InitializeComponent();
    }

    public IViewGithubLoginPresenter Presenter { get; set; }

    public void Run()
    {
        ShowDialog();
    }

    private void AuthenticateButton_Click(object sender, EventArgs e)
    {
        Settings.Default.GithubToken = _githubToken.Text;
        Settings.Default.Save();

        Close();
    }

    private void githubTokenHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var githubTokenHelpLink =
            "https://github.com/salihozkara/MetricHunter/blob/master/doc/UserGuide.md#how-to-authenticate";

        var ps = new ProcessStartInfo(githubTokenHelpLink)
        {
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(ps);
    }
}