using System.Globalization;
using JsonNet.ContractResolvers;
using MetricHunter.Core.Paths;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MetricHunter.Core.Jsons;

public class JsonHelper
{
    private static readonly JsonSerializerSettings ReadingJsonSerializerSettings = new()
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
    
    public static Task WriteJsonAsync<T>(T obj, FilePath path)
    {
        var json = JsonConvert.SerializeObject(obj);
        path.CreateDirectoryIfNotExists();
        return File.WriteAllTextAsync(path, json);
    }
    
    public static async Task<T?> ReadJsonAsync<T>(FilePath path)
    {
        if (!path.Exists) return default;
        var json = await File.ReadAllTextAsync(path);
        return ReadJson<T?>(json);
    }
    
    public static T? ReadJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T?>(json, ReadingJsonSerializerSettings);
    }
    
    public static Task AppendJsonAsync<T>(T obj, FilePath path)
    {
        if (!path.Exists) return WriteJsonAsync(obj, path);
        var json = ReadJson<T>(path);
        var list = new List<T> { json, obj };
        return WriteJsonAsync(list, path);

    }

    public static async Task AppendRangeJsonAsync<T>(IEnumerable<T> obj, FilePath path)
    {
        if (path.Exists)
        {
            var json = await ReadJsonAsync<T>(path);
            if (json != null)
            {
                obj = obj.Append(json);
            }
            else
            {
                var listJson = await ReadJsonAsync<IEnumerable<T>>(path);
                if (listJson != null)
                {
                    obj = obj.Concat(listJson);
                }
                else
                {
                    throw new Exception("Could not read json");
                }
            }
        }
        
        await WriteJsonAsync(obj, path);
    }
}