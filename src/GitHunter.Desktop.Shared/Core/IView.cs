namespace GitHunter.Desktop.Core;

public interface IView<TPresenter> : IView where TPresenter : IPresenter
{
    TPresenter Presenter { get; set; }
}

public interface IView
{
    void Run();
}