using System.Reflection;
using MetricHunter.Core;
using MetricHunter.Core.Paths;

namespace MetricHunter.Application.Resources;

// TODO: Refactor
public static class Resource
{
    private static readonly DirectoryPath ResFolder = "Res";

    private static readonly DirectoryPath DynamicResFolder = AppDomain.CurrentDomain.BaseDirectory + ResFolder;

    private static string GetOrCreateResFolder()
    {
        var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var privateSlnFolderName = "." + MetricHunterConsts.AppName.ToLower();
        var resFolder = Path.Combine(userFolder, privateSlnFolderName, ResFolder);
        if (!Directory.Exists(resFolder)) Directory.CreateDirectory(resFolder);
        var baseResFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, ResFolder);
        foreach (var file in Directory.GetFiles(baseResFolder, "*.*", SearchOption.AllDirectories))
        {
            var relativePath = file[(baseResFolder.Length + 1)..];
            var targetFile = Path.Combine(resFolder, relativePath);
            if (File.Exists(targetFile)) continue;
            var targetFolder = Path.GetDirectoryName(targetFile);
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder!);
            File.Copy(file, targetFile, true);
        }

        return resFolder;
    }

    public static class SourceMonitor
    {
        public static readonly FilePath TemplateXml = $"{DynamicResFolder}/SourceMonitor/template.xml";

        public static FilePath SourceMonitorExe => $"{DynamicResFolder}/SourceMonitor/SourceMonitor.exe";
    }
}