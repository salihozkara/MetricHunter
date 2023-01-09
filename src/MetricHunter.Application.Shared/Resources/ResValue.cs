namespace MetricHunter.Application.Resources;

public struct ResValue<T>
{
    public string Path { get; }
    public T Value { get; }

    public ResValue(string path, T value)
    {
        Path = System.IO.Path.GetFullPath(path);
        Value = value;
    }
}