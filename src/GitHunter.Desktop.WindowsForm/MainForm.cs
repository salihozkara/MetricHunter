using Volo.Abp.DependencyInjection;

namespace GitHunter.Desktop.WindowsForm;

public partial class MainForm : Form, ISingletonDependency
{
    public MainForm()
    {
        InitializeComponent();
    }
}