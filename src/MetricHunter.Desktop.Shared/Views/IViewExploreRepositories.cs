using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Presenters;
using Octokit;

namespace MetricHunter.Desktop.Views;

public interface IViewExploreRepositories : IView<IViewExploreRepositoriesPresenter>
{
    
    Language? SelectedLanguage { get; }

    SortDirection SortDirection { get; }

    int RepositoryCount { get; }

    string Topics { get; }
    
    IEnumerable<Language>? LanguageSelectList { set; }

    IEnumerable<SortDirection> SortDirectionSelectList { set; }
}