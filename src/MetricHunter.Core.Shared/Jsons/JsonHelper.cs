using System.Collections;
using System.Collections.Concurrent;
using System.Globalization;
using AdvancedPath;
using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace MetricHunter.Core.Jsons;

public static class JsonHelper
{
    private static ConcurrentDictionary<string,object> _lockers = new();
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
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

    private static Task WriteJsonAsync<T>(T obj, FilePathString path, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var json = obj is IEnumerable ? JsonConvert.SerializeObject(obj, JsonSerializerSettings) : JsonConvert.SerializeObject(new [] { obj }, JsonSerializerSettings);
        path.ParentDirectory.CreateIfNotExists();
        
        // lock
        var locker = _lockers.GetOrAdd(path, new object());
        lock (locker)
        {
            File.WriteAllText(path, json);
        }
        
        return Task.CompletedTask;
    }

    public static async Task<T?> ReadJsonAsync<T>(FilePathString path, CancellationToken cancellationToken = default)
        where T : class
    {
        cancellationToken.ThrowIfCancellationRequested();
        try
        {
            if (!path.Exists)
            {
                return default;
            }

            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(typeof(T)) && typeof(T) != typeof(string);
            
            // lock
            var locker = _lockers.GetOrAdd(path, new object());
            string json;
            lock (locker)
            {
                json = File.ReadAllText(path);
            }
            
            var jToken = JToken.Parse(json);
            if (isEnumerable)
            {
                if (jToken is JArray jArray)
                {
                    return jArray.ToObject<T>(JsonSerializer.Create(JsonSerializerSettings));
                }

                var type = typeof(T);
                var itemType =
                    (type.HasElementType ? type.GetElementType() :
                        type.IsGenericType ? type.GetGenericArguments()[0] : typeof(object)) ?? typeof(object);
                var value = jToken.ToObject(itemType, JsonSerializer.Create(JsonSerializerSettings));
                try
                {
                    var array = Array.CreateInstance(itemType, 1);
                    array.SetValue(value, 0);
                    if (array is T result)
                    {
                        return result;
                    }

                    if (type.IsClass)
                    {
                        result = (T)Activator.CreateInstance(type, array)!;
                    }
                    else if (type.IsInterface)
                    {
                        result = (T)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType), array)!;
                    }
                    else
                    {
                        throw new NotSupportedException($"Type {type} is not supported");
                    }

                    return result;
                }
                catch (Exception e)
                {
                    return default;
                }
            }
            else
            {
                if (jToken is not JArray jArray)
                {
                    return jToken.ToObject<T>(JsonSerializer.Create(JsonSerializerSettings));
                }

                var array = jArray.ToObject<T[]>(JsonSerializer.Create(JsonSerializerSettings));
                return array is not null && array.Length > 0 ? array[0] : default;

            }
        }
        catch
        {
            return default;
        }
    }

    public static async Task AppendJsonAsync<T, TKey>(T obj, FilePathString path, Func<T, TKey>? distinctBy,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (!path.Exists)
        {
            await WriteJsonAsync(obj, path, cancellationToken);
            return;
        }

        var result = await ReadJsonAsync<List<T>>(path, cancellationToken);
        if (result != null)
        {
            result.Add(obj);
            if (distinctBy != null) result = result.DistinctBy(distinctBy).ToList();
            await WriteJsonAsync(result, path, cancellationToken);
            return;
        }

        await WriteJsonAsync(obj, path, cancellationToken);
    }

    public static Task AppendJsonAsync<T>(T obj, FilePathString path, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return AppendJsonAsync<T, object>(obj, path, null, cancellationToken);
    }

    public static async Task AppendRangeJsonAsync<T>(IEnumerable<T> obj, FilePathString path,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (path.Exists)
        {
            var result = await ReadJsonAsync<List<T>>(path, cancellationToken);
            if (result != null) obj = obj.Concat(result);
        }

        await WriteJsonAsync(obj, path, cancellationToken);
    }
}