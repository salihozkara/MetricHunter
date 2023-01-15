namespace MetricHunter.Core.Paths;

public abstract class MetricHunterPath : FileSystemInfo
{
    protected virtual FileSystemInfo Info => this;

    private static MetricHunterPath Create(string path)
    {
        if (File.Exists(path)) return new FilePath(path);
        if (Directory.Exists(path)) return new DirectoryPath(path);

        return new AnonymousPath(path);
    }

    protected string Path { get; }

    protected MetricHunterPath(string path)
    {
        Path = path;
    }

    public override string ToString() => Path;

    public virtual DirectoryPath ParentDirectory => new (System.IO.Path.GetDirectoryName(Path)!);
    
    public virtual void CreateDirectory()
    {
        DirectoryPath? directory = System.IO.Path.GetDirectoryName(FullPath);
        directory?.CreateDirectory();
    }

    public static implicit operator string(MetricHunterPath path) => path.Path;

    public static implicit operator MetricHunterPath(string path) => Create(path);

    public override bool Equals(object? obj) =>
        FullPath.Equals(System.IO.Path.GetFullPath(obj?.ToString() ?? string.Empty),
            StringComparison.OrdinalIgnoreCase);

    protected bool Equals(MetricHunterPath other) =>
        FullPath.Equals(other.FullPath, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode()
    {
        return HashCode.Combine(Path, Info);
    }

    public override void Delete() => Info.Delete();
    public override string Name => Info.Name;
    public override bool Exists => Info.Exists;
}

public abstract class MetricHunterPath<T> : MetricHunterPath where T : MetricHunterPath<T>
{
    protected MetricHunterPath(string path) : base(path)
    {
    }

    private static AnonymousPath FromMetricHunterPath(MetricHunterPath<T>? path) => new (path);

    public static AnonymousPath operator +(MetricHunterPath<T> path, object other) =>
        FromMetricHunterPath(System.IO.Path.Combine(path.Path, other.ToString() ?? string.Empty));
    
    public static AnonymousPath operator /(MetricHunterPath<T> path, object other) => path + other ;
    
    public static implicit operator string(MetricHunterPath<T> path) => path.Path;
    public static implicit operator MetricHunterPath<T>?(string? path) => path == null ? null : Create(path);
    
    private static T Create(string path)
    {
        return (T) Activator.CreateInstance(typeof(T), path)!;
    }
}