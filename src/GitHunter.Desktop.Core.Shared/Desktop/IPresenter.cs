namespace GitHunter.Core.Desktop;

public interface IPresenter<TView>
{
    TView View { get; }
    void Run();
}
