namespace MetricHunter.Application.Git;

public class ExceptionEventArgs
{
    public ExceptionEventArgs(Exception exception)
    {
        Exception = exception;
        Handled = false;
        ThrowException = false;
        DefaultExceptionHandling = true;
        Retry = false;
    }

    public Exception Exception { get; set; }
    public bool Handled { get; set; }
    public bool ThrowException { get; set; }
    public bool DefaultExceptionHandling { get; set; }
    public bool Retry { get; set; }
}