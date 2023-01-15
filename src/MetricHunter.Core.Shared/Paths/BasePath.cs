namespace MetricHunter.Core.Paths;

public abstract class BasePath
{
    protected readonly string _path;
    
    protected FileSystemInfo? FileSystemInfo;

    public bool IsFullPath => Path.IsPathRooted(_path);
    public string FullPath => Path.GetFullPath(_path);
    
    public virtual DirectoryPath Directory => Path.GetDirectoryName(FullPath)!;
    
    public bool Exists => IsValid && (File.Exists(_path) || System.IO.Directory.Exists(_path));
    
    public virtual DirectoryPath ParentDirectory => System.IO.Directory.GetParent(_path)?.FullName!;
    public bool IsValid => ValidatePath();
    
    private PathType? _pathType;
    public PathType Type => _pathType ??= GetPathType();

    public virtual void CreateIfNotExists(){}

    public void CreateDirectoryIfNotExists()
    {
        Directory.CreateIfNotExists();
    }
    
    public void DeleteIfExists()
    {
        if (!Exists)
            return;
        
        if (Type == PathType.File)
            File.Delete(_path);
        else
            System.IO.Directory.Delete(_path);
    }

    private bool ValidatePath()
    {
        return !string.IsNullOrWhiteSpace(_path) && !Path.GetInvalidPathChars().Any(_path.Contains);
    }

    private PathType GetPathType()
    {
        if(!IsValid) return PathType.Invalid;
        if(Exists)
        {
            return File.Exists(_path) ? PathType.File : PathType.Directory;
        }

        return PathType.Unknown;
    }
    
    protected BasePath(string path)
    {
        _path = path;
    }

    public static implicit operator string(BasePath pathBase)
    {
        return pathBase._path;
    }
    
    public static implicit operator BasePath(string path)
    {
        var unknownPath = new UnknownPath(path);
        return unknownPath.Type switch
        {
            PathType.File => new FilePath(path),
            PathType.Directory => new DirectoryPath(path),
            PathType.Invalid => new InvalidPath(),
            _ => unknownPath
        };
    }

    public static UnknownPath operator +(BasePath pathBase, object path)
    {
        return Path.Combine(pathBase._path, path.ToString()!);
    }
    
    public static UnknownPath operator /(BasePath pathBase, object path) => pathBase + path;
    
    public override bool Equals(object? obj) =>
        FullPath.Equals(Path.GetFullPath(obj?.ToString() ?? string.Empty),
            StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode()
    {
        return FullPath.GetHashCode();
    }

    protected bool Equals(BasePath other) =>
        FullPath.Equals(other.FullPath, StringComparison.OrdinalIgnoreCase);
    public override string ToString() => this;
}