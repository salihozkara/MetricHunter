using AdvancedPath;

namespace MetricHunter.Core.Paths;

public static class PathHelper
{
    private const string ReportsPrefix = "Reports";
    private const string RepositoriesPrefix = "Repositories";
    public static readonly DirectoryPathString TempPath = (Path.GetTempPath() + "MetricHunter\\" + Guid.NewGuid().ToString("N"))!;
    public static readonly DirectoryPathString AppDataPath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MetricHunter\\")!;

    private static PathString BuildPath(params string?[] paths)
    {
        var newPaths = paths.Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => p!).ToArray();
        var path = Path.Combine(newPaths);
        return path;
    }

    public static DirectoryPathString BuildDirectoryPath(params string?[] paths)
    {
        return BuildPath(paths).ToDirectoryPathString();
    }

    public static FilePathString BuildReportPath(string reportPath, string language, string fullName,string branchName, string extension)
    {
        var fileName = $"{fullName}-{branchName}.{extension}";
        var path = BuildPath(reportPath, language, ReportsPrefix, fileName);
        return path.ToFilePathString();
    }

    public static DirectoryPathString BuildRepositoryDirectoryPath(string repositoryPath, string language, string fullname, string branchName = "")
    {
        var path = BuildPath(repositoryPath, language, RepositoriesPrefix, fullname, branchName);
        return path.ToDirectoryPathString();
    }

    public static void DeleteTempFiles()
    {
        if (!Directory.Exists(TempPath)) return;
        var files = Directory.GetFiles(TempPath, "*", SearchOption.AllDirectories);

        files.AsParallel().ForAll(f =>
        {
            try
            {
                File.SetAttributes(f, FileAttributes.Normal);
                File.Delete(f);
            }
            catch
            {
                // ignored
            }
        });

        try
        {
            Directory.Delete(TempPath, true);
        }
        catch
        {
            // ignored
        }
    }
}