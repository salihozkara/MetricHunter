using Octokit;

namespace MetricHunter.Application.Git;

public class RequestFinishedEventArgs
{
    public RequestFinishedEventArgs(IReadOnlyList<Repository> repositories,
        HashSet<SearchRepositoriesRequest> failedRequests)
    {
        Repositories = repositories;
        FailedRequests = failedRequests;
    }

    public IReadOnlyList<Repository> Repositories { get; }
    public HashSet<SearchRepositoriesRequest> FailedRequests { get; }
}