using System.Diagnostics;

namespace GitHunter.Core.Processes;

public interface IProcessManager
{
    Task<ProcessResult> RunAsync(string command, string arguments, string? workingDirectory = null);
    Task<ProcessResult> RunAsync(Process process, string arguments, string? workingDirectory = null);

    Task<ProcessResult> RunAsync(ProcessStartInfo processStartInfo);

    Task<bool> KillAllProcessesAsync();
    void KillAllProcesses();
}