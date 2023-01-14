namespace MetricHunter.Core.Paths;

public class FilePath : MetricHunterPath<FilePath>
{
    private readonly FileInfo _info;
    protected override FileSystemInfo Info => _info;
    public override DirectoryPath ParentDirectory => new (_info.DirectoryName!);

    public FilePath(string path) : this(new FileInfo(path))
    {
    }

    public FilePath(FileInfo fileInfo) : base(fileInfo.FullName)
    {
        _info = fileInfo;
    }

    public string FileName => System.IO.Path.GetFileName(Path);

    public string FileNameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(Path);
    
    public static implicit operator FilePath?(AnonymousPath? path) => path is null ? null : new FilePath(path!);
    public static implicit operator FilePath?(FileInfo? path) => path == null ? null : new FilePath(path);
    public static implicit operator string(FilePath path) => path.Path;
}