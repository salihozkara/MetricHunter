using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using JsonNet.ContractResolvers;
using MetricHunter.Core;
using MetricHunter.Core.Paths;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MetricHunter.Application.Resources;

// TODO: Refactor
public static class Resource
{
    private const string ResFolder = "Res";

    private static readonly DirectoryPath DynamicResFolder = "./" + ResFolder;

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

    public static class Jsons
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.Auto,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            MaxDepth = 100,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DateParseHandling = DateParseHandling.DateTimeOffset,
            FloatFormatHandling = FloatFormatHandling.String,
            FloatParseHandling = FloatParseHandling.Double,
            StringEscapeHandling = StringEscapeHandling.Default,
            Culture = CultureInfo.InvariantCulture,
            CheckAdditionalContent = true,
            Converters = new JsonConverter[] { new StringEnumConverter() },
            ContractResolver = new PrivateSetterContractResolver()
        };
    }
}