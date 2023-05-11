using MetricHunter.Desktop.Core;
using MetricHunter.Desktop.Presenters;
using Octokit;
using Language = MetricHunter.Core.Languages.Language;

namespace MetricHunter.Desktop.Views;

public interface IViewExploreRepositories : IView<IViewExploreRepositoriesPresenter>
{
    Language? SelectedLanguage { get; }

    SortDirection SortDirection { get; }

    int RepositoryCount { get; }

    string Topics { get; }
    
    IEnumerable<MetricHunter.Core.Languages.Language>? LanguageSelectList { set; }

    IEnumerable<SortDirection> SortDirectionSelectList { set; }
    
    void Close();
}