namespace MetricHunter.Core.Paths;

public static class PathHelper
{
    public static readonly string TempPath = Path.GetTempPath() + "MetricHunter\\";
    private const string ReportsPrefix = "Reports";
    private const string RepositoriesPrefix = "Repositories";

    private static string BuildPath(params string?[] paths)
    {
        var newPaths = paths.Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => p!).ToArray();
        var path = Path.Combine(newPaths);
        return path;
    }
    
    public static string BuildDirectoryPath(params string?[] paths)
    {
        var newPaths = paths.Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => p!).ToArray();
        var path = Path.Combine(newPaths);
        return path;
    }

    public static string BuildAndCreateFullPath(params string[] paths)
    {
        var path = BuildPath(paths);
        Directory.CreateDirectory(path);

        return path!;
    }

    public static string BuildReportPath(string reportPath, string language, string fullName,
        string extension)
    {
        var fileName = $"{fullName}.{extension}";
        var path = BuildPath(reportPath, language, ReportsPrefix, fileName);
        return path!;
    }

    public static string BuildRepositoryDirectoryPath(string repositoryPath, string language, string fullname)
    {
        var path = BuildPath(repositoryPath, language, RepositoriesPrefix, fullname);
        return path!;
    }
    
    public static string BuildRandomTempDirectoryPath()
    {
        var path = TempPath + Guid.NewGuid();
        return path;
    }
}