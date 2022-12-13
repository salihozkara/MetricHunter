using Octokit;

namespace GitHunter.Application.Git;

public class CloneRepositorySuccessEventArgs
{
    public CloneRepositorySuccessEventArgs(Repository repository)
    {
        Repository = repository;
    }

    public Repository Repository { get; set; }
}