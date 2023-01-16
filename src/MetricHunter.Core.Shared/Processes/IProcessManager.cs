namespace MetricHunter.Core.Processes;

public interface IProcessManager
{
    Task<ProcessResult> RunAsync(ProcessStartInfo processStartInfo, CancellationToken cancellationToken = default);

    void KillAllProcesses(bool allowNewProcesses = false);
}