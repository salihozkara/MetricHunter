namespace MetricHunter.Core.Paths;

public class FilePath : BasePath
{
    public FilePath(string path) : base(path)
    {
    }

    public FileInfo FileInfo
    {
        get
        {
            if (FileSystemInfo is not FileInfo fileInfo)
                FileSystemInfo = fileInfo = new FileInfo(_path);
            return fileInfo;
        }
    }

    public override DirectoryPath ParentDirectory => Directory;

    public string FileName => Path.GetFileName(_path);

    public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(_path);

    public string Extension => Path.GetExtension(_path);

    public static implicit operator FilePath(FileInfo fileSystemInfo)
    {
        var path = new FilePath(fileSystemInfo.FullName)
        {
            FileSystemInfo = fileSystemInfo
        };
        return path;
    }

    public static IEnumerable<FilePath> FromFileInfoEnumerable(IEnumerable<FileInfo> fileSystemInfos)
    {
        return fileSystemInfos.Select(x => (FilePath)x);
    }

    public static IEnumerable<FilePath> FromStringEnumerable(IEnumerable<string> paths)
    {
        return paths.Select(x => new FilePath(x));
    }

    public static implicit operator FilePath(UnknownPath unknownPath)
    {
        return new(unknownPath);
    }

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
}