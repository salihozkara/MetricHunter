namespace MetricHunter.Core.Processes;

public class ProcessResult
{
    public int ExitCode { get; set; }
    public string? Output { get; set; }
    public string? Error { get; set; }
}