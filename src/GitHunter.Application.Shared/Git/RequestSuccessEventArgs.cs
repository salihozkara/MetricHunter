using Octokit;

namespace GitHunter.Application.Git;

public class RequestSuccessEventArgs : EventArgs
{
    public RequestSuccessEventArgs(SearchRepositoriesRequest request,
        SearchRepositoryResult result)
    {
        Request = request;
        Result = result;
    }

    public SearchRepositoriesRequest Request { get; }
    public SearchRepositoryResult Result { get; }
}