namespace GitHunter.Application.Git;

public interface IGitManager
{
    void Initialize(string username, string password);
    void Initialize(string token);
}