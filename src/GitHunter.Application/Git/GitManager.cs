using GitHunter.Core.DependencyProcesses;
using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Application.Git;

[ProcessDependency<GitProcessDependency>]
public class GitManager : IGitManager, IDependencyProcess, ITransientDependency
{
    private ILogger<GitManager> _logger;
    private static readonly GitHubClient Client = new(new ProductHeaderValue("GitHunter"));

    public GitManager(ILogger<GitManager> logger)
    {
        _logger = logger;
    }
    
    private static void Initialize(Credentials? credentials = null)
    {
        if (credentials != null)
        {
            Client.Credentials = credentials;
        }
    }
    
    private async Task<GitOutput> GetRepositories(GitInput input)
    {
        var request = new SearchRepositoriesRequest()
        {
            Language = input.Language,
            Topic = input.Topic,
            SortField = RepoSearchSort.Stars,
            Page = input.Page,
            PerPage = input.PerPage,
        };
        var result = await Client.Search.SearchRepo(request);
        return new GitOutput(result.Items, result.TotalCount, result.IncompleteResults);
    }

    public void Initialize(string username, string password)
    {
        Initialize(new Credentials(username, password));
    }

    public void Initialize(string token)
    {
        Initialize(new Credentials(token));
    }
}

public class GitInput
{
    public Language? Language { get; set; }
    public string? Location { get; set; }
    public string? User { get; set; }
    public string? Repository { get; set; }
    public string? Topic { get; set; }
    public string? Keyword { get; set; }
    public string? License { get; set; }
    public string? Sort { get; set; }
    public string? Order { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
}

public class GitOutput
{
    public GitOutput(IReadOnlyList<Repository> resultItems, int resultTotalCount, bool resultIncompleteResults)
    {
        Repositories = resultItems;
        TotalCount = resultTotalCount;
        IncompleteResults = resultIncompleteResults;
    }

    public bool IncompleteResults { get; }

    public int TotalCount { get; }

    public IReadOnlyList<Repository> Repositories { get; }
    
}