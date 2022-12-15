using Octokit;

namespace GitHunter.Application.Git;

public class SearchRepositoriesRequestSuccessEventArgs : EventArgs
{
    public SearchRepositoriesRequestSuccessEventArgs(SearchRepositoriesRequest searchRepositoriesRequest,
        SearchRepositoryResult searchRepositoryResult)
    {
        SearchRepositoriesRequest = searchRepositoriesRequest;
        SearchRepositoryResult = searchRepositoryResult;
    }

    public SearchRepositoriesRequest SearchRepositoriesRequest { get; }
    public SearchRepositoryResult SearchRepositoryResult { get; }
}