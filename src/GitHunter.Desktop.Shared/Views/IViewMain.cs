using GitHunter.Desktop.Core;
using GitHunter.Desktop.Models;
using GitHunter.Desktop.Presenters;
using Octokit;

namespace GitHunter.Desktop.Views;

public interface IViewMain : IView<IViewMainPresenter>
{
    void ShowMessage(string message);
    string GithubToken { get; }
    IEnumerable<Language>? LanguageSelectList { set; }

    IEnumerable<SortDirection> SortDirectionSelectList { set; }

    IEnumerable<long> SelectedRepositories { get; }

    Language? SelectedLanguage { get; }

    SortDirection SortDirection { get; }

    int RepositoryCount { get; }

    string Topics { get; }

    string JsonLoadPath { get; set; }
    
    string JsonSavePath { get; set; }
    
    string DownloadRepositoryPath { get; set; }

    void ShowRepositories(IEnumerable<RepositoryModel> repositories);
}
