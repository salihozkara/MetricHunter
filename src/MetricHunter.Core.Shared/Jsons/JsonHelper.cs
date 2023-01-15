using System.Collections;
using System.Globalization;
using JsonNet.ContractResolvers;
using MetricHunter.Core.Paths;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace MetricHunter.Core.Jsons;

public static class JsonHelper
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
    
    public static async Task<T?> ReadJsonAsync<T>(FilePath path) where T : class
    {
        var isEnumerable = typeof(IEnumerable).IsAssignableFrom(typeof(T));
        if (!path.Exists) return default;
        var json = await File.ReadAllTextAsync(path);
        var jToken = JToken.Parse(json);
        if (isEnumerable)
        {
            if(jToken is JArray jArray)
            {
                return jArray.ToObject<T>(JsonSerializer.Create(ReadingJsonSerializerSettings));
            }

            var type = typeof(T);
            var itemType = (type.HasElementType ? type.GetElementType() : type.IsGenericType ? type.GetGenericArguments()[0] : typeof(object)) ?? typeof(object);
            var value = jToken.ToObject(itemType, JsonSerializer.Create(ReadingJsonSerializerSettings));
            var array = Array.CreateInstance(itemType, 1);
            array.SetValue(value, 0);
            if (array is T result) return result;
            if (type.IsClass)
            {
                result = (T)Activator.CreateInstance(type, array)!;
            }else if(type.IsInterface)
            {
                result = (T)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType), array)!;
            }else
            {
                throw new NotSupportedException($"Type {type} is not supported");
            }
            return result;
        }
        else
        {
            if(jToken is JArray jArray)
            {
                var array = jArray.ToObject<List<T>>(JsonSerializer.Create(ReadingJsonSerializerSettings));
                return array is not null && array.Count > 0 ? array[0] : default;
            }

            return jToken.ToObject<T>(JsonSerializer.Create(ReadingJsonSerializerSettings));
        }
    }

    public static async Task AppendJson<T, TKey>(T obj, FilePath path, Func<T,TKey>? distinctBy)
    {
        if (!path.Exists)
        {
            await WriteJsonAsync(obj, path);
            return;
        }

        var result = await ReadJsonAsync<List<T>>(path);
        if (result != null)
        {
            result.Add(obj);
            if (distinctBy != null) result = result.DistinctBy(distinctBy).ToList();
            await WriteJsonAsync(result, path);
            return;
        }

        await WriteJsonAsync(obj, path);
    }
    
    public static Task AppendJson<T>(T obj, FilePath path)
    {
        return AppendJson<T,object>(obj, path, null);
    }

    public static async Task AppendRangeJsonAsync<T>(IEnumerable<T> obj, FilePath path)
    {
        if (path.Exists)
        {
            var result = await ReadJsonAsync<List<T>>(path);
            if (result != null)
            {
                obj = obj.Concat(result);
            }
        }
        
        await WriteJsonAsync(obj, path);
    }
}