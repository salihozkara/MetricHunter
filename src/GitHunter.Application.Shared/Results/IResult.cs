namespace GitHunter.Application.Results;

public interface IResult
{
    Dictionary<string, string> ToDictionary();

    List<Dictionary<string, string>> ToDictionaryListByTopics();
}