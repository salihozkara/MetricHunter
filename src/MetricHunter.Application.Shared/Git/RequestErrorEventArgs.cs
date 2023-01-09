using Octokit;

namespace MetricHunter.Application.Git;

public class RequestErrorEventArgs : EventArgs
{
    public RequestErrorEventArgs(SearchRepositoriesRequest request,
        Exception? exception = null)
    {
        Request = request;
        Exception = exception;
    }

    public SearchRepositoriesRequest Request { get; }
    public Exception? Exception { get; }
}