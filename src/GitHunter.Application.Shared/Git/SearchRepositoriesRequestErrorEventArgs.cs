using Octokit;

namespace GitHunter.Application.Git;

public class SearchRepositoriesRequestErrorEventArgs : EventArgs
{
    public SearchRepositoriesRequestErrorEventArgs(SearchRepositoriesRequest searchRepositoriesRequest,
        Exception? exception = null)
    {
        SearchRepositoriesRequest = searchRepositoriesRequest;
        Exception = exception;
    }

    public SearchRepositoriesRequest SearchRepositoriesRequest { get; }
    public Exception? Exception { get; }
}