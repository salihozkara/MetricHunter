namespace GitHunter.Desktop.Core;

public interface IPresenter<TView>
{
    TView View { get; }
    void Run();
}
