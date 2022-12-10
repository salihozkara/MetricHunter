using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop;

public partial class MainForm : Form, ISingletonDependency
{
    public MainForm()
    {
        InitializeComponent();
    }
}