using Octokit;

namespace MetricHunter.Application.Git;

public class CloneRepositorySuccessEventArgs
{
    public CloneRepositorySuccessEventArgs(Repository repository, string localPath)
    {
        Repository = repository;
        LocalPath = localPath;
    }

    public Repository Repository { get; set; }
    
    public string LocalPath { get; set; }
}