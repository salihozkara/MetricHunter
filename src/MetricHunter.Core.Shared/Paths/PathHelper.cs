namespace MetricHunter.Core.Paths;

public static class PathHelper
{
    private const string ReportsPrefix = "Reports";
    private const string RepositoriesPrefix = "Repositories";
    public static readonly DirectoryPath TempPath = (Path.GetTempPath() + "MetricHunter\\")!;

    private static UnknownPath BuildPath(params string?[] paths)
    {
        var newPaths = paths.Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => p!).ToArray();
        var path = Path.Combine(newPaths);
        return path;
    }

    public static DirectoryPath BuildDirectoryPath(params string?[] paths)
    {
        return BuildPath(paths);
    }

    public static FilePath BuildReportPath(string reportPath, string language, string fullName,
        string extension)
    {
        var fileName = $"{fullName}.{extension}";
        var path = BuildPath(reportPath, language, ReportsPrefix, fileName);
        return path;
    }

    public static DirectoryPath BuildRepositoryDirectoryPath(string repositoryPath, string language, string fullname)
    {
        var path = BuildPath(repositoryPath, language, RepositoriesPrefix, fullname);
        return path;
    }
}