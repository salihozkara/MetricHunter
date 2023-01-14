namespace MetricHunter.Core.Paths;

public class DirectoryPath : MetricHunterPath<DirectoryPath>
{
    public readonly DirectoryInfo DirectoryInfo;
    public DirectoryPath(string path) : base(path)
    {
        DirectoryInfo = new DirectoryInfo(path);
    }

    private DirectoryPath(DirectoryInfo directoryDirectoryInfo) : base(directoryDirectoryInfo.FullName)
    {
        DirectoryInfo = directoryDirectoryInfo;
    }

    protected override FileSystemInfo Info => DirectoryInfo;
    public override DirectoryPath ParentDirectory => new(Directory.GetParent(Path)!);

    public void CreateIfNotExists()
    {
        if (!Exists) DirectoryInfo.Create();
    }

    public static implicit operator DirectoryPath?(AnonymousPath? path) => path is null ? null : new DirectoryPath(path!);
    public static implicit operator DirectoryPath?(string? path) => string.IsNullOrWhiteSpace(path) ? null : new DirectoryPath(path);
    public static implicit operator DirectoryPath?(DirectoryInfo? info) => info is null ? null : new DirectoryPath(info);
}