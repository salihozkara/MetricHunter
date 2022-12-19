using System.Collections.Immutable;
using System.Diagnostics;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Core.Processes;

public class ProcessManager : IProcessManager, ISingletonDependency
{
    private readonly List<Process> _processes = new();

    private bool _killAllProcessesRequested;

    public Task<ProcessResult> RunAsync(string command, string arguments, string? workingDirectory = null)
    {
        return RunAsync(new ProcessStartInfo(command, arguments, workingDirectory));
    }

    public async Task<ProcessResult> RunAsync(Process process, string arguments, string? workingDirectory = null)
    {
        if (_killAllProcessesRequested)
            throw new InvalidOperationException(
                "Cannot run a new process after KillAllProcessesAsync() has been called.");

        string? output = null;
        string? error = null;

        process.StartInfo.Arguments = arguments;
        if (string.IsNullOrWhiteSpace(process.StartInfo.WorkingDirectory))
            process.StartInfo.WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

        process.ErrorDataReceived += (sender, args) =>
        {
            if (args.Data == null) return;
            error += args.Data + Environment.NewLine;
        };

        process.OutputDataReceived += (sender, args) =>
        {
            if (args.Data == null) return;
            output += args.Data + Environment.NewLine;
        };


        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        _processes.Add(process);

        await process.WaitForExitAsync();

        var result = new ProcessResult
        {
            ExitCode = process.ExitCode,
            Output = output,
            Error = error
        };

        return result;
    }


    public async Task<ProcessResult> RunAsync(ProcessStartInfo processStartInfo)
    {
        if (_killAllProcessesRequested)
            throw new InvalidOperationException(
                "Cannot run a new process after KillAllProcessesAsync() has been called.");

        var process = CreateProcess(processStartInfo);

        string? output = null;
        string? error = null;

        process.ErrorDataReceived += (sender, args) =>
        {
            if (args.Data == null) return;
            error += args.Data + Environment.NewLine;
            processStartInfo.ErrorDataReceived?.Invoke(args.Data);
        };

        process.OutputDataReceived += (sender, args) =>
        {
            if (args.Data == null) return;
            output += args.Data + Environment.NewLine;
            processStartInfo.OutputDataReceived?.Invoke(args.Data);
        };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        _processes.Add(process);

        await process.WaitForExitAsync();
        processStartInfo.Exited?.Invoke();

        return new ProcessResult
        {
            ExitCode = process.ExitCode,
            Output = output,
            Error = error
        };
    }

    public Task<bool> KillAllProcessesAsync()
    {
        _killAllProcessesRequested = true;
        var processes = _processes.Where(process => !process.HasExited).ToImmutableArray();
        foreach (var process in processes)
        {
            process.Kill(true);
            process.WaitForExit();
            process.Close();
            process.Dispose();
            _processes.Remove(process);
        }

        return Task.FromResult(true);
    }

    public bool KillAllProcesses()
    {
        _killAllProcessesRequested = true;
        var processes = _processes.Where(process => !process.HasExited).ToImmutableArray();
        foreach (var process in processes)
        {
            process.Kill(true);
            process.WaitForExit();
            process.Close();
            process.Dispose();
            _processes.Remove(process);
        }

        return true;
    }

    private Process CreateProcess(ProcessStartInfo processStartInfo)
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