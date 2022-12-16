using GitHunter.Desktop.Core;
using GitHunter.Desktop.Models;
using GitHunter.Desktop.Presenters;
using Octokit;

namespace GitHunter.Desktop.Views;

public interface IViewMain : IView<IViewMainPresenter>
{
    IEnumerable<Language>? LanguageSelectList { set; }
    IEnumerable<SortDirection> SortDirectionSelectList { set; }
    Language? SelectedLanguage { get; }
    SortDirection SortDirection { get;}
    int RepositoryCount { get; }
    string Topics { get; }
    void ShowRepositories(IEnumerable<RepositoryModel> repositories);
}