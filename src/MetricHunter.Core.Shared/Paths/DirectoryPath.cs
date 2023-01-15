namespace MetricHunter.Core.Paths;

public class DirectoryPath : BasePath
{
    
    public DirectoryInfo DirectoryInfo {
        get
        {
            if(FileSystemInfo is not DirectoryInfo directoryInfo)
                FileSystemInfo = directoryInfo = new DirectoryInfo(_path);
            return directoryInfo;
        }
    }
    public static implicit operator DirectoryPath(DirectoryInfo fileSystemInfo)
    {
        var path = new DirectoryPath(fileSystemInfo.FullName)
        {
            FileSystemInfo = fileSystemInfo
        };
        return path;
    }

    public override DirectoryPath Directory => this;

    public DirectoryPath(string path) : base(path)
    {
    }
    
    public static implicit operator DirectoryPath(UnknownPath unknownPath) => new(unknownPath);
    public static implicit operator string(DirectoryPath pathBase)
    {
        return pathBase._path;
    }
    
    public static implicit operator DirectoryPath(string path)
    {
        return new DirectoryPath(path);
    }

    public override void CreateIfNotExists()
    {
        if(Exists) return;
        System.IO.Directory.CreateDirectory(_path);
    }
}