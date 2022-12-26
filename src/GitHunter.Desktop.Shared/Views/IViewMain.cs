using GitHunter.Desktop.Core;
using GitHunter.Desktop.Models;
using GitHunter.Desktop.Presenters;
using Octokit;

namespace GitHunter.Desktop.Views;

public interface IViewMain : IView<IViewMainPresenter>
{
    IEnumerable<Language>? LanguageSelectList { set; }

    IEnumerable<SortDirection> SortDirectionSelectList { set; }

    IEnumerable<long> SelectedRepositories { get; }

    Language? SelectedLanguage { get; }

    SortDirection SortDirection { get; }

    int RepositoryCount { get; }

    string Topics { get; }

    string RepositoriesJsonPath { get; set; }
    string RepositoriesFolderPath { get; set; }
    string DownloadPath { get; set; }

    void ShowRepositories(IEnumerable<RepositoryModel> repositories);
}
