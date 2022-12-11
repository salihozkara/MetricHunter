using Octokit;

namespace GitHunter.Application.Git;

public class GitOutput
{
    public GitOutput(IReadOnlyList<Repository> resultItems, int resultTotalCount, bool resultIncompleteResults)
    {
        Repositories = resultItems;
        TotalCount = resultTotalCount;
        IncompleteResults = resultIncompleteResults;
    }

    public bool IncompleteResults { get; }

    public int TotalCount { get; }

    public IReadOnlyList<Repository> Repositories { get; }
}