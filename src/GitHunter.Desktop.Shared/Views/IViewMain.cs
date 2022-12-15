using GitHunter.Core.Desktop;
using GitHunter.Desktop.Presenters;
using Octokit;

namespace GitHunter.Desktop.Views;

public interface IViewMain : IView<IViewMainPresenter>
{
    Language? SelectedLanguage { get; }
    SortDirection SortDirection { get;}
    int RepositoryCount { get; }
    void ShowRepositories(IEnumerable<Repository> repositories);
}