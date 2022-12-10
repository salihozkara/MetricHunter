namespace GitHunter.Core.Processes;

public class ProcessStartInfo
{
    public ProcessStartInfo(string command, string arguments, string? workingDirectory = null, Action? exited = null,
        Action<string>? outputDataReceived = null, Action<string>? errorDataReceived = null)
    {
        Command = command;
        Arguments = arguments;
        Exited = exited;
        WorkingDirectory = workingDirectory;
        OutputDataReceived = outputDataReceived;
        ErrorDataReceived = errorDataReceived;
    }

    public string Command { get; private set; }
    public string Arguments { get; private set; }
    public string? WorkingDirectory { get; private set; }
    public Action<string>? OutputDataReceived { get; private set; }
    public Action<string>? ErrorDataReceived { get; private set; }
    public Action? Exited { get; private set; }
}