using Octokit;

namespace GitHunter.Application.Git;

public class GitInput
{
    public Language? Language { get; set; }
    public string? Topic { get; set; }
    public int Count { get; set; }
}