using GitHunter.Application.Git;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public partial class MainForm : Form, ISingletonDependency
{
    private readonly IGitManager _githubManager;
    public MainForm(IGitManager githubManager)
    {
        _githubManager = githubManager;
        InitializeComponent();
    }
}