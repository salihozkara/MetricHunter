using GitHunter.Application.Git;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public partial class MainForm : Form, ISingletonDependency
{
    private readonly IGitManager _githubManager;

    public MainForm(IGitManager githubManager)
    {
        _githubManager = githubManager;
        Load += MainForm_Load;

        InitializeComponent();
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        // var results = await _githubManager.GetRepositories(new GitInput()
        // {
        //     Count = 5796,
        //     Language = Language.CSharp,
        //     Topic = "android"
        // }, () =>
        // {
        //     MessageBox.Show("Rate limit exceeded!");
        // });
    }
}