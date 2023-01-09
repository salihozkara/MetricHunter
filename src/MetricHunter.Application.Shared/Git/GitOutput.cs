using Octokit;

namespace MetricHunter.Application.Git;

public class GitOutput
{
    public GitOutput(IReadOnlyList<Repository> resultItems, IReadOnlyList<SearchRepositoriesRequest> failedRequests)
    {
        Repositories = resultItems;
        FailedRequests = failedRequests;
    }

    public IReadOnlyList<Repository> Repositories { get; }
    public IReadOnlyList<SearchRepositoriesRequest> FailedRequests { get; }
}