namespace MetricHunter.Core.Paths;

public class FilePath : BasePath
{
    public FilePath(string path) : base(path)
    {
    }
    
    public FileInfo FileInfo {
        get
        {
            if(FileSystemInfo is not FileInfo fileInfo)
                FileSystemInfo = fileInfo = new FileInfo(_path);
            return fileInfo;
        }
    }
    
    public static implicit operator FilePath(FileInfo fileSystemInfo)
    {
        var path = new FilePath(fileSystemInfo.FullName)
        {
            FileSystemInfo = fileSystemInfo
        };
        return path;
    }

    public override DirectoryPath ParentDirectory => Directory;

    public static implicit operator FilePath(UnknownPath unknownPath) => new(unknownPath);
    public static implicit operator string(FilePath pathBase)
    {
        return pathBase._path;
    }
    
    public static implicit operator FilePath(string path)
    {
        return new UnknownPath(path);
    }

    public override void CreateIfNotExists()
    {
        CreateDirectoryIfNotExists();
        File.Create(_path).Close();
    }

    public string FileName => Path.GetFileName(_path);
    
    public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(_path);
    
    public string Extension => Path.GetExtension(_path);
}