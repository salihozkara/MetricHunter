namespace GitHunter.Core.Helpers;

public static class PathHelper
{
    public static string BuildPath(params string?[] paths)
    {
        var newPaths = paths.Where(p=>p!=null).Select(p=>p!).ToArray();
        var path = Path.Combine(newPaths);
        return path;
    }

    public static string BuildFullPath(params string[] paths)
    {
        return Path.GetFullPath(BuildPath(paths));
    }

    public static string BuildAndCreateFullPath(params string[] paths)
    {
        var path = BuildFullPath(paths);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        return path;
    }
}