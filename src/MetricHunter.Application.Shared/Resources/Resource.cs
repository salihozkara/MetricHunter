using AdvancedPath;
using MetricHunter.Core.Paths;

namespace MetricHunter.Application.Resources;

public static class Resource
{
    private static readonly string ResFolder = "Res";

    public static readonly DirectoryPathString DynamicResFolder = (PathHelper.TempPath + ResFolder).ToDirectoryPathString();


    public static Func<string, FilePathString> GetOrCreateFile;

    public static class SourceMonitor
    {
        public static readonly FilePathString TemplateXml = GetOrCreateFile($"{DynamicResFolder}/SourceMonitor/template.xml");

        public static FilePathString SourceMonitorExe => GetOrCreateFile($"{DynamicResFolder}/SourceMonitor/SourceMonitor.exe");
    }
}