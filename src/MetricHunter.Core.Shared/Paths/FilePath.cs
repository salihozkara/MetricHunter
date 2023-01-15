namespace MetricHunter.Core.Paths;

public class FilePath : MetricHunterPath<FilePath>
{
    public readonly FileInfo FileInfo;
    protected override FileSystemInfo Info => FileInfo;
    public override DirectoryPath ParentDirectory => new (FileInfo.DirectoryName!);

    public FilePath(string path) : this(new FileInfo(path))
    {
    }

    public FilePath(FileInfo fileFileInfo) : base(fileFileInfo.FullName)
    {
        FileInfo = fileFileInfo;
    }

    public string FileName => System.IO.Path.GetFileName(Path);

    public string FileNameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(Path);
    
    public static implicit operator FilePath?(AnonymousPath? path) => path is null ? null : new FilePath(path!);
    public static implicit operator FilePath?(FileInfo? path) => path == null ? null : new FilePath(path);
    public static implicit operator string(FilePath path) => path.Path;

    public void CreateIfNotExists()
    {
        if (!Exists)
        {
            FileInfo.Create().Close();
        }
    }
}