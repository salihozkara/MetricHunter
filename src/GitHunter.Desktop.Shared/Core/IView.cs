namespace GitHunter.Desktop.Core;

public interface IView<TPresenter>
{
    TPresenter Presenter { get; set; }
    void Run();
}