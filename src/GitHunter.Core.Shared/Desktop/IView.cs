namespace GitHunter.Core.Desktop;

public interface IView<TPresenter>
{
    TPresenter Presenter { get; set; }
    void Run();
}