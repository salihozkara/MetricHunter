using Octokit;

namespace GitHunter.Application.Git;

public class SearchRepositoriesRequestSuccessEventArgs : EventArgs
{
    public SearchRepositoriesRequest SearchRepositoriesRequest { get; }
    public IReadOnlyList<Repository> Repositories { get; }

    public SearchRepositoriesRequestSuccessEventArgs(SearchRepositoriesRequest searchRepositoriesRequest,
        IReadOnlyList<Repository> repositories)
    {
        SearchRepositoriesRequest = searchRepositoriesRequest;
        Repositories = repositories;
    }
}