using GitHunter.Desktop.Core;
using GitHunter.Desktop.Views;

namespace GitHunter.Desktop.Presenters;

public interface IViewMainPresenter : IPresenter<IViewMain>
{

     void LoadForm();
     Task SearchRepositories();
     Task<string> CalculateMetrics();
     Task DownloadMetrics();
     
     void LoadRepositoriesFromFiles(string path);
     void SaveRepositoriesToFile(string fileName);
}