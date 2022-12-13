using Octokit;

namespace GitHunter.Application.Git;

public class SearchRepositoriesRequestFinishedEventArgs
{
    public SearchRepositoriesRequestFinishedEventArgs(IReadOnlyList<Repository> repositories,
        List<SearchRepositoriesRequest> failedRequests, List<List<SearchRepositoriesRequest>>? waitingRequests = null)
    {
        Repositories = repositories;
        FailedRequests = failedRequests;
        WaitingRequests = waitingRequests ?? new List<List<SearchRepositoriesRequest>>();
    }

    public IReadOnlyList<Repository> Repositories { get; }
    public List<SearchRepositoriesRequest> FailedRequests { get; }

    public List<List<SearchRepositoriesRequest>> WaitingRequests { get; }
}