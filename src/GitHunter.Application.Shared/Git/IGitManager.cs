using Octokit;

namespace GitHunter.Application.Git;

public interface IGitManager
{
    void Initialize(string username, string password);
    void Initialize(string token);
    Task<GitOutput> GetRepositories(GitInput input);
    Task<GitOutput> RetryFailedRequest();
    IReadOnlyList<Repository> GetAllSuccessRepositories { get; }
    
    void Clear();
    
    event EventHandler<SearchRepositoriesRequestErrorEventArgs>? SearchRepositoriesRequestError;
    event EventHandler<SearchRepositoriesRequestSuccessEventArgs>? SearchRepositoriesRequestSuccess;
    event EventHandler<SearchRepositoriesRequestFinishedEventArgs>? SearchRepositoriesRequestFinished;
}

public class SearchRepositoriesRequestFinishedEventArgs
{
    public IReadOnlyList<Repository> Repositories { get; set; }
    public List<SearchRepositoriesRequest> FailedRequests { get; set; }
    
    public List<List<SearchRepositoriesRequest>> WaitingRequests { get; set; }

    public SearchRepositoriesRequestFinishedEventArgs(IReadOnlyList<Repository> repositories, List<SearchRepositoriesRequest> failedRequests, List<List<SearchRepositoriesRequest>> waitingRequests)
    {
        Repositories = repositories;
        FailedRequests = failedRequests;
        WaitingRequests = waitingRequests;
    }
}

public interface IGitProvider
{
    void Initialize(string username, string password);
    void Initialize(string token);
    Task<bool> CloneRepository(Repository repository, CancellationToken token = default);
    event EventHandler<CloneRepositoryErrorEventArgs>? CloneRepositoryError;
    event EventHandler<CloneRepositorySuccessEventArgs>? CloneRepositorySuccess;

    Task<bool> DeleteLocalRepository(Repository repository, CancellationToken token = default);
    Task<bool> DeleteLocalRepository(string path, CancellationToken token = default);
}

public class CloneRepositorySuccessEventArgs
{
    public Repository Repository { get; set; }
    
    public CloneRepositorySuccessEventArgs(Repository repository)
    {
        Repository = repository;
    }
}

public class CloneRepositoryErrorEventArgs
{
    public Repository Repository { get; set; }
    public Exception? Exception { get; set; }

    public CloneRepositoryErrorEventArgs(Repository repository, Exception? exception)
    {
        Repository = repository;
        Exception = exception;
    }
}