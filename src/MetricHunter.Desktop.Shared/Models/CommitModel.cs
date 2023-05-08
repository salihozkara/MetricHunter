namespace MetricHunter.Desktop.Models;

public class CommitModel
{
    public int Index { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public string Author { get; set; }
    
    public DateTime CommitedAt { get; set; }

}