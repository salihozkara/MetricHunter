using MetricHunter.Core.Tasks;
using MetricHunter.Desktop.Presenters;
using MetricHunter.Desktop.Views;
using Octokit;

namespace MetricHunter.Desktop.Forms
{
    public partial class ViewExploreRepositories : Form, IViewExploreRepositories
    {
        public ViewExploreRepositories()
        {
            InitializeComponent();
        }

        public void Run()
        {
            Presenter.LoadForm();
            ShowDialog();
        }

        public CancellationTokenSource CancellationTokenSource { get; set; }

        public IViewExploreRepositoriesPresenter Presenter { get; set; }

        public Language? SelectedLanguage => _languageComboBox.SelectedValue as Language?;

        public SortDirection SortDirection =>
            _sortDirectionComboBox.SelectedValue as SortDirection? ?? SortDirection.Descending;

        public int RepositoryCount =>
            int.TryParse(_repositoryCountTextBox.Text, out var repositoryCount) ? repositoryCount : 10;

        public string Topics => _topicsTextBox.Text;

        public IEnumerable<Language>? LanguageSelectList
        {
            set => _languageComboBox.DataSource = value;
        }

        public IEnumerable<SortDirection> SortDirectionSelectList
        {
            set => _sortDirectionComboBox.DataSource = value;
        }

        private async void _searchButton_Click(object sender, EventArgs e)
        {
            CancellationTokenSource = new CancellationTokenSource();
            await Presenter.SearchRepositoriesAsync(CancellationTokenSource.Token)
                .MaybeCanceled(CancellationTokenSource.Token);
        }
    }
}
