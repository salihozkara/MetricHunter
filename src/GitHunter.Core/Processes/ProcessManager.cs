using System.Diagnostics;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Core.Processes;

public class ProcessManager : IProcessManager, IScopedDependency
{
    private readonly List<Process> _processes = new();

    private bool _killAllProcessesRequested;

    public Task<ProcessResult> RunAsync(string command, string arguments, string? workingDirectory = null) =>
        RunAsync(new ProcessStartInfo(command, arguments, workingDirectory));

    public Task<ProcessResult> RunAsync(Process process, string arguments, string? workingDirectory = null)
    {
        if (_killAllProcessesRequested)
        {
            throw new InvalidOperationException(
                "Cannot run a new process after KillAllProcessesAsync() has been called.");
        }
        var tcs = new TaskCompletionSource<ProcessResult>();
        
        string? output = null;
        string? error = null;

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

        process.Exited += (sender, args) =>
        {
            var result = new ProcessResult
            {
                ExitCode = process.ExitCode,
                Output = output,
                Error = error
            };

            tcs.SetResult(result);
        };
        
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        
        process.Start();

        _processes.Add(process);

        return tcs.Task;
        
    }


    public Task<ProcessResult> RunAsync(ProcessStartInfo processStartInfo)
    {
        if (_killAllProcessesRequested)
        {
            throw new InvalidOperationException(
                "Cannot run a new process after KillAllProcessesAsync() has been called.");
        }

        var process = CreateProcess(processStartInfo);

        var tcs = new TaskCompletionSource<ProcessResult>();
        
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

        process.Exited += (sender, args) =>
        {
            processStartInfo.Exited?.Invoke();
            var result = new ProcessResult
            {
                ExitCode = process.ExitCode,
                Output = output,
                Error = error
            };

            tcs.SetResult(result);
        };
        
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        
        process.Start();

        _processes.Add(process);

        return tcs.Task;
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

    public Task<bool> KillAllProcessesAsync()
    {
        _killAllProcessesRequested = true;
        foreach (var process in _processes.Where(process => !process.HasExited))
        {
            process.Kill();
        }

        return Task.FromResult(true);
    }
}