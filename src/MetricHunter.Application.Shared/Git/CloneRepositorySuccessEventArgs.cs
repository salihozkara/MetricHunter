using MetricHunter.Core.Paths;
using Octokit;

namespace MetricHunter.Application.Git;

public class CloneRepositorySuccessEventArgs
{
    public CloneRepositorySuccessEventArgs(Repository repository, DirectoryPath localPath)
    {
        Repository = repository;
        LocalPath = localPath;
    }

    public Repository Repository { get; set; }

    public DirectoryPath LocalPath { get; set; }
}