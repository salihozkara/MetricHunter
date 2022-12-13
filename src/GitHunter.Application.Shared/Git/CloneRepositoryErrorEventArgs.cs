using Octokit;

namespace GitHunter.Application.Git;

public class CloneRepositoryErrorEventArgs
{
    public CloneRepositoryErrorEventArgs(Repository repository, Exception? exception)
    {
        Repository = repository;
        Exception = exception;
    }

    public Repository Repository { get; set; }
    public Exception? Exception { get; set; }
}