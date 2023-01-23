using AdvancedPath;
using Octokit;

namespace MetricHunter.Application.Git;

public class CloneRepositorySuccessEventArgs
{
    public CloneRepositorySuccessEventArgs(Repository repository, DirectoryPathString localPath)
    {
        Repository = repository;
        LocalPath = localPath;
    }

    public Repository Repository { get; set; }

    public DirectoryPathString LocalPath { get; set; }
}