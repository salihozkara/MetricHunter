namespace MetricHunter.Desktop.Core;

public interface IPresenter<TView> : IPresenter where TView : IView
{
    TView View { get; }
}

public interface IPresenter
{
    void Run();
}