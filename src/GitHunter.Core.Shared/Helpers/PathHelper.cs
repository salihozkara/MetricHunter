namespace GitHunter.Core.Helpers;

public static class PathHelper
{
    public static string BuildPath(params string[] paths)
    {
        paths = paths.Append("./").ToArray();
        var path = Path.Combine(paths);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        return path;
    }

    public static string BuildFullPath(params string[] paths)
    {
        return Path.GetFullPath(BuildPath(paths));
    }
}