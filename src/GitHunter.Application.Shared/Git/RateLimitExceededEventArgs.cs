namespace GitHunter.Application.Git;

public class RateLimitExceededEventArgs
{
    public RateLimitExceededEventArgs(DateTimeOffset reset)
    {
        Reset = reset;
        Wait = true;
    }

    public bool Wait { get; set; }
    public DateTimeOffset Reset { get; }
}