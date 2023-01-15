namespace MetricHunter.Core.Paths;

public class UnknownPath : BasePath
{
    public UnknownPath(string path) : base(path)
    {
    }
    
    public static implicit operator string(UnknownPath pathBase)
    {
        return pathBase._path;
    }
    
    public static implicit operator UnknownPath(string path)
    {
        return new UnknownPath(path);
    }
    
    public static implicit operator UnknownPath(FileSystemInfo fileSystemInfo)
    {
        var unknownPath = new UnknownPath(fileSystemInfo.FullName)
        {
            FileSystemInfo = fileSystemInfo
        };
        return unknownPath;
    }
}