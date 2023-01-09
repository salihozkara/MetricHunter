using Octokit;

namespace MetricHunter.Application.Git;

public class CloneRepositorySuccessEventArgs
{
    public CloneRepositorySuccessEventArgs(Repository repository)
    {
        Repository = repository;
    }

    public Repository Repository { get; set; }
}