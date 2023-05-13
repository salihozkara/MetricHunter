
namespace MetricHunter.Core.Tasks;

public static class TaskExtensions
{
    public static async Task<T?> MaybeCanceled<T>(this Task<T> task, CancellationToken cancellationToken,
        T? defaultValue = default)
    {
        try
        {
            return await task;
        }
        catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            return defaultValue;
        }
    }

    public static async Task MaybeCanceled(this Task task, CancellationToken cancellationToken)
    {
        try
        {
            await task.WaitAsync(cancellationToken);
        }
        catch (TaskCanceledException e) when (cancellationToken.IsCancellationRequested)
        {
            // ignore
        }
    }
}