using MetricHunter.Desktop.Presenters;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Forms {
    public partial class ViewFindRepository : Form, IViewFindRepository {
        public ViewFindRepository() {
            InitializeComponent();
            _repositories = new List<Repository>();
        }

        private IReadOnlyList<Repository> _repositories;

        public void Run() {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Show();

        }

        public IViewFindRepositoryPresenter Presenter { get; set; }

        private void _findButton_Click(object sender, EventArgs e) {
            Presenter.FindRepository();
        }

        public string RepositoryFullNameOrUrl => _repositoryNameTextBox.Text;

        public void ShowRepository(Repository repository) {
            Presenter.Repository = repository;
            _repositoryNameTextBox.Text = repository.FullName;
            Height = 448;
            _repositoryPanel.Visible = true;
            _repositoryImage.ImageLocation = repository.Owner.AvatarUrl;
            _repositoryName.Text = repository.Name;
            _repositoryOwner.Text = repository.Owner.Login;
            _repositoryDescription.Text = repository.Description;

            if (repository.Description.IsNullOrWhiteSpace()) {
                _repositoryDescription.Text = "No description";
            }

            _repositoryUrl.Text = repository.HtmlUrl;
            label3.Text = GetCountString(repository.StargazersCount);

            label5.Text = GetCountString(repository.ForksCount);

            label6.Text = ToSizeString(repository.Size);

            CenterToScreen();
        }

        private static string ToSizeString(long size) // size in kilobytes
        {
            return size switch {
                < 1024 => $"{size} KB",
                < 1024 * 1024 => $"{Math.Round(size / 1024.0, 2)} MB",
                _ => $"{Math.Round(size / 1024.0 / 1024.0, 2)} GB"
            };
        }

        private string GetCountString(int count) {
            if (count < 1000) return count.ToString();
            if (count < 1000000) return $"{count / 1000}k";
            return $"{count / 1000000}m";
        }

        private void _commitsButton_Click(object sender, EventArgs e) {
            Presenter.GetCommits();
        }

        private void _releasesButton_Click(object sender, EventArgs e) {
            Presenter.GetReleases();
        }

        private async void _repositoryNameTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (_repositoryNameTextBox.Text.IsNullOrWhiteSpace()) return;
                _findButton.PerformClick();
                return;
            }
            if (e.KeyCode != Keys.Divide) return;

            var owner = _repositoryNameTextBox.Text.Split('/')[0];

            try {
                _repositories = await Presenter.GitManager.GetRepositoriesByOwnerAsync(owner);
                _repositoryNameTextBox.AutoCompleteCustomSource.AddRange(_repositories.Select(x => x.FullName).ToArray());
            }
            catch {
                // ignored
            }
        }
    }
}
