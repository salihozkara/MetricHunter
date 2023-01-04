using System.Xml;
using GitHunter.Application.Resources;
using GitHunter.Application.Results;
using GitHunter.Core.DependencyProcesses;
using GitHunter.Core.Helpers;
using GitHunter.Core.Processes;
using Microsoft.Extensions.Logging;
using Octokit;

namespace GitHunter.Application.Metrics.SourceMonitor;

[Language(Language.CSharp, Language.CPlusPlus, Language.Java)]
[ProcessDependency<SourceMonitorProcessDependency>]
public class SourceMonitorMetricCalculator : IMetricCalculator
{
    private const string ProjectNameReplacement = "{{project_name}}";
    private const string ProjectDirectoryReplacement = "{{project_directory}}";
    private const string ProjectFileDirectoryReplacement = "{{project_file_directory}}";
    private const string ProjectLanguageReplacement = "{{project_language}}";
    private const string ReportsPathReplacement = "{{reports_path}}";

    private const string ReportsPath = "Reports";

    private readonly ILogger<SourceMonitorMetricCalculator> _logger;
    private readonly IProcessManager _processManager;


    public SourceMonitorMetricCalculator(IProcessManager processManager,
        ILogger<SourceMonitorMetricCalculator> logger)
    {
        _processManager = processManager;
        _logger = logger;
    }


    public async Task<IResult> CalculateMetricsAsync(Repository repository, CancellationToken token = default)
    {
        await ProcessRepository(repository, token);
        var reportsPath =
            PathHelper.BuildFullPath(repository.Language, ReportsPath, repository.FullName + ".xml");

        var xmlDocument = new XmlDocument();
        xmlDocument.Load(reportsPath);

        AddIdToXml(repository, xmlDocument, reportsPath);

        FileNameChangeAndMove(repository, reportsPath);

        return new SourceMonitorResult(repository, GetMetrics(xmlDocument));
    }

    private List<IMetric> GetMetrics(XmlNode xmlNode)
    {
        List<IMetric> metrics = new();

        var metricsDetails = xmlNode
            .SelectNodes("//metric_name")?.Cast<XmlNode>()
            .Zip(xmlNode.SelectNodes("//metric")?.Cast<XmlNode>() ?? Array.Empty<XmlNode>(),
                (name, value) => new { name, value }).ToDictionary(k => k.name, v => v.value);
        var matchesMetrics = metricsDetails?.Keys
            .Select(k => new Metric(k.InnerText, metricsDetails[k].InnerText)).ToList();
        if (matchesMetrics != null) metrics.AddRange(matchesMetrics);

        return Metrics(metrics);
    }

    private List<IMetric> Metrics(List<IMetric> metrics)
    {
        return new List<IMetric>
        {
            new LinesMetric(GetMetricListValue(metrics, LinesMetric.MatchedMetricNames)),
            new StatementsMetric(GetMetricListValue(metrics, StatementsMetric.MatchedMetricNames)),
            new PercentCommentLinesMetric(GetMetricListValue(metrics, PercentCommentLinesMetric.MatchedMetricNames)),
            new PercentDocumentationLinesMetric(GetMetricListValue(metrics,
                PercentDocumentationLinesMetric.MatchedMetricNames)),
            new ClassesInterfacesStructsMetric(GetMetricListValue(metrics,
                ClassesInterfacesStructsMetric.MatchedMetricNames)),
            new MethodsPerClassMetric(GetMetricListValue(metrics, MethodsPerClassMetric.MatchedMetricNames)),
            new StatementsPerMethodMetric(GetMetricListValue(metrics, StatementsPerMethodMetric.MatchedMetricNames)),
            new MaximumComplexityMetric(GetMetricListValue(metrics, MaximumComplexityMetric.MatchedMetricNames)),
            new AverageComplexityMetric(GetMetricListValue(metrics, AverageComplexityMetric.MatchedMetricNames)),
            new MaximumBlockDepthMetric(GetMetricListValue(metrics, MaximumBlockDepthMetric.MatchedMetricNames)),
            new AverageBlockDepthMetric(GetMetricListValue(metrics, AverageBlockDepthMetric.MatchedMetricNames))
        };
    }

    private static string GetMetricListValue(IEnumerable<IMetric> metrics, string[] match)
    {
        var metric = metrics.FirstOrDefault(m => match.Contains(m.Name));
        return metric?.Value ?? "0";
    }

    private static void FileNameChangeAndMove(Repository repository, string reportsPath)
    {
        // file name change and move
        var fileInfo = new FileInfo(reportsPath);
        var newFileName = $"id_{repository.Id}_{fileInfo.Name}";
        var newFilePath = Path.Combine(Resource.SourceMonitor.XmlReportsFolder, newFileName);
        if (File.Exists(newFilePath))
            File.Delete(newFilePath);
        fileInfo.MoveTo(newFilePath);
    }

    private static void AddIdToXml(Repository repository, XmlDocument xmlDocument, string reportsPath)
    {
        // add id to xml
        var root = xmlDocument.DocumentElement;
        var idAttribute = xmlDocument.CreateAttribute("id");
        idAttribute.Value = repository.Id.ToString();
        root?.Attributes.Append(idAttribute);
        xmlDocument.Save(reportsPath);
    }

    private async Task ProcessRepository(Repository repository, CancellationToken token = default)
    {
        if (token.IsCancellationRequested)
            return;
        var reportsPath = Path.Combine(repository.Language, ReportsPath, repository.FullName + ".xml");
        if (File.Exists(reportsPath))
        {
            _logger.LogInformation("Reports already exist for {RepositoryName}. Skipping...", repository.FullName);
            return;
        }

        await CalculateStatisticsUsingSourceMonitor(repository);
    }

    private async Task CalculateStatisticsUsingSourceMonitor(Repository repository)
    {
        _logger.LogInformation("Calculating statistics for {RepositoryName}", repository.FullName);
        var xmlPath = await CreateSourceMonitorXml(repository);

        var result = await _processManager.RunAsync(Resource.SourceMonitor.SourceMonitorExe.Value, $"/C \"{xmlPath}\"");
        _logger.LogDebug("SourceMonitor log: {SourceMonitorLog}", result.Output);
        _logger.LogError("SourceMonitor error log: {SourceMonitorErrorLog}", result.Error);
        if (result.ExitCode == 0)
            _logger.LogInformation("Statistics for {RepositoryName} calculated successfully", repository.FullName);
        else
            _logger.LogError("Error while calculating statistics for {RepositoryName}", repository.FullName);
    }

    private async Task<string> CreateSourceMonitorXml(Repository repository)
    {
        var xmlDirectory =
            PathHelper.BuildAndCreateFullPath(repository.Language, "SourceMonitor", repository.Owner.Login);

        var reportsPath = PathHelper.BuildAndCreateFullPath(repository.Language, "Reports", repository.Owner.Login);

        var projectDirectory = PathHelper.BuildFullPath(repository.Language + " Repositories", repository.FullName);

        var xmlPath = Path.Combine(xmlDirectory, $"{repository.Name}.xml");

        if (File.Exists(xmlPath))
        {
            _logger.LogInformation("SourceMonitor xml file already exists for {RepositoryName}. Skipping...",
                repository.FullName);
            return xmlPath;
        }

        var xml = Resource.SourceMonitor.TemplateXml.Value
            .Replace(ProjectNameReplacement, repository.Name)
            .Replace(ProjectDirectoryReplacement, projectDirectory)
            .Replace(ProjectFileDirectoryReplacement, xmlDirectory)
            .Replace(ProjectLanguageReplacement, repository.Language)
            .Replace(ReportsPathReplacement, reportsPath);
        await File.WriteAllTextAsync(xmlPath, xml);
        return xmlPath;
    }
}