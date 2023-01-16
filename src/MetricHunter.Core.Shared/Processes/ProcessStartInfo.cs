using MetricHunter.Core.Paths;

namespace MetricHunter.Core.Processes;

public class ProcessStartInfo
{
    public ProcessStartInfo(string command, string arguments, DirectoryPath? workingDirectory = null,
        Action? exited = null,
        Action<string>? outputDataReceived = null, Action<string>? errorDataReceived = null)
    {
        Command = command;
        Arguments = arguments;
        Exited = exited;
        WorkingDirectory = workingDirectory;
        OutputDataReceived = outputDataReceived;
        ErrorDataReceived = errorDataReceived;

        workingDirectory?.CreateIfNotExists();
    }

    public string Command { get; }
    public string Arguments { get; }
    public DirectoryPath? WorkingDirectory { get; }
    public Action<string>? OutputDataReceived { get; }
    public Action<string>? ErrorDataReceived { get; }
    public Action? Exited { get; }
}