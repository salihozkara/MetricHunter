namespace MetricHunter.Desktop.Models;

public class ReleaseModel
{
    public int Index { get; set; }

    public string Name { get; set; }
    
    public string Url { get; set; }

    public DateTime? PublishedAt { get; set; }
}