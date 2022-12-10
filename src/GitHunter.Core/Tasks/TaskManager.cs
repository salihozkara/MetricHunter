namespace GitHunter.Core.Tasks;

public class TaskManager
{
    private readonly Dictionary<Guid,List<Task>> _tasks = new();

    public Task RunTask(List<Task> tasks, Func<Exception,bool>? exceptionHandler = null)
    {
        _tasks.Add(Guid.NewGuid(), tasks);
        return Task.WhenAll(tasks).ContinueWith(t =>
        {
            if (!t.IsFaulted) return;
            if (exceptionHandler == null || !t.Exception!.InnerExceptions.Any(exceptionHandler))
            {
                throw t.Exception!;
            }
        });
    }
}