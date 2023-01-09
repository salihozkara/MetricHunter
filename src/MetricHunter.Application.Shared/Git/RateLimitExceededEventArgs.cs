namespace MetricHunter.Application.Git;

public class RateLimitExceededEventArgs
{
    public RateLimitExceededEventArgs(DateTimeOffset resetTimeOffset)
    {
        ResetTimeOffset = resetTimeOffset;
        Wait = true;
    }

    public bool Wait { get; set; }
    public DateTimeOffset ResetTimeOffset { get; }
}