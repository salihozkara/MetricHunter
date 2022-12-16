using GitHunter.Application.Git;
using GitHunter.Core.Desktop;
using GitHunter.Desktop.Views;

namespace GitHunter.Desktop.Presenters;

public interface IViewMainPresenter : IPresenter<IViewMain>
{
     void LoadForm();
     Task SearchRepositories();
}