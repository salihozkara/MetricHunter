namespace MetricHunter.Core.Paths;

public class AnonymousPath : MetricHunterPath<AnonymousPath>
{
    public override DirectoryPath ParentDirectory => new (System.IO.Path.GetDirectoryName(Path)!);

    public AnonymousPath(string path) : base(path)
    {
    }

    public static implicit operator AnonymousPath?(string? path) => string.IsNullOrWhiteSpace(path) ? null : new AnonymousPath(path);
}