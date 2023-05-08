using MetricHunter.Desktop.Presenters;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Forms
{
    public partial class ViewFindRepository : Form, IViewFindRepository
    {
        public ViewFindRepository()
        {
            InitializeComponent();
        }

        public void Run()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Show();

        }

        public IViewFindRepositoryPresenter Presenter { get; set; }

        private void _findButton_Click(object sender, EventArgs e)
        {
            Presenter.FindRepository();
        }

        public string RepositoryFullNameOrUrl => _repositoryNameTextBox.Text;
                
        public void ShowRepository(Repository repository)
        {
            Height = 750;
            _repositoryPanel.Visible = true;
            _repositoryImage.ImageLocation = repository.Owner.AvatarUrl;
            _repositoryName.Text = repository.Name;
            _repositoryOwner.Text = repository.Owner.Login;
            _repositoryDescription.Text = repository.Description;

            if (repository.Description.IsNullOrWhiteSpace())
            {
                _repositoryDescription.Text = "No description";
            }
            
            _repositoryUrl.Text = repository.HtmlUrl;
            
            CenterToScreen();
        }

        private void _commitsButton_Click(object sender, EventArgs e)
        {
            Presenter.GetCommits();
        }

        private void _releasesButton_Click(object sender, EventArgs e)
        {
            Presenter.GetReleases();
        }
    }
}
