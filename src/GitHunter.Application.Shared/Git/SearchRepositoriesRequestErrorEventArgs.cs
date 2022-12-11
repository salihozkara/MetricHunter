using Octokit;

namespace GitHunter.Application.Git;

public class SearchRepositoriesRequestErrorEventArgs : EventArgs
{
    public SearchRepositoriesRequest SearchRepositoriesRequest { get; }
    public Exception? Exception { get; set; }

    public CancellationTokenSource CancellationTokenSource { get; }

    public SearchRepositoriesRequestErrorEventArgs(SearchRepositoriesRequest searchRepositoriesRequest,
        CancellationTokenSource cancellationTokenSource, Exception? exception = null)
    {
        SearchRepositoriesRequest = searchRepositoriesRequest;
        CancellationTokenSource = cancellationTokenSource;
        Exception = exception;
    }
}