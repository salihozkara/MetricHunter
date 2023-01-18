using System.Diagnostics;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Core.Processes;

public class ProcessManager : IProcessManager, ISingletonDependency
{
    private readonly List<Process> _processes = new();

    private bool _killAllProcessesRequested;

    public async Task<ProcessResult> RunAsync(ProcessStartInfo processStartInfo,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_killAllProcessesRequested)
            throw new InvalidOperationException(
                "Cannot run a new process after KillAllProcessesAsync() has been called.");

        var process = CreateProcess(processStartInfo);

        string? output = null;
        string? error = null;

        process.ErrorDataReceived += (_, args) =>
        {
            if (args.Data == null) return;
            error += args.Data + Environment.NewLine;
            processStartInfo.ErrorDataReceived?.Invoke(args.Data);
        };

        process.OutputDataReceived += (_, args) =>
        {
            if (args.Data == null) return;
            output += args.Data + Environment.NewLine;
            processStartInfo.OutputDataReceived?.Invoke(args.Data);
        };

        try
        {
            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            _processes.Add(process);

            await process.WaitForExitAsync(cancellationToken);
            processStartInfo.Exited?.Invoke();
        }
        catch (Exception e)
        {
            ProcessKill(process);
        }

        return new ProcessResult
        {
            ExitCode = process.ExitCode,
            Output = output,
            Error = error
        };
    }

    public void KillAllProcesses(bool allowNewProcesses = false)
    {
        _killAllProcessesRequested = true;
        var processes = _processes.ToList();
        foreach (var process in processes)
        {
            ProcessKill(process);
            _processes.Remove(process);
        }

        _killAllProcessesRequested = !allowNewProcesses;
    }

    private void ProcessKill(Process process)
    {
        process.Kill(true);
        process.WaitForExit();
        process.Close();
        process.Dispose();
        _processes.Remove(process);
    }

    private static Process CreateProcess(ProcessStartInfo processStartInfo)
    {
        return new Process
        {
            StartInfo =
            {
                Arguments = processStartInfo.Arguments,
                FileName = processStartInfo.Command,
                WorkingDirectory = processStartInfo.WorkingDirectory ?? Directory.GetCurrentDirectory(),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            },
            EnableRaisingEvents = true
        };
    }
}