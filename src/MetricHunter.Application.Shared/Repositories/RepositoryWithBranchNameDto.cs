using Octokit;

namespace MetricHunter.Application.Repositories;

public class RepositoryWithBranchNameDto
{
    public string Key { get; }
    public Repository Repository { get; }
    public string BranchName { get; }
    
    public object? OtherData { get; set; }
    
    public RepositoryWithBranchNameDto(Repository? repository, string? branchName = null, object? otherData = null)
    {
        if(repository is null)
            return;
        
        Repository = repository;
        BranchName = branchName ?? repository.DefaultBranch;
        OtherData = otherData;
        
        Key = otherData switch
        {
            null => repository.Id.ToString(),
            GitHubCommit commit => commit.Sha,
            Release release => release.TagName,
            _ => throw new ArgumentOutOfRangeException(nameof(otherData))
        };
    }
    
    public override string ToString()
    {
        return $"{Repository.FullName} {BranchName} {Key}";
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is not RepositoryWithBranchNameDto repositoryWithBranchNameDto)
        {
            return false;
        }

        return Repository.Id == repositoryWithBranchNameDto.Repository.Id && BranchName == repositoryWithBranchNameDto.BranchName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Repository.Id, BranchName);
    }
}