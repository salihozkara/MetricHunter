﻿using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using AdvancedPath;
using MetricHunter.Application.Git;
using MetricHunter.Application.Repositories;
using MetricHunter.Application.Resources;
using MetricHunter.Application.Results;
using MetricHunter.Core.DependencyProcesses;
using MetricHunter.Core.Languages;
using MetricHunter.Core.Paths;
using MetricHunter.Core.Processes;
using Microsoft.Extensions.Logging;
using Octokit;
using Octokit.Internal;
using Language = MetricHunter.Core.Languages.Language;

namespace MetricHunter.Application.Metrics.SourceMonitor;

[Language(Language.C, Language.CPlusPlus, Language.CSharp, Language.Delphi, Language.Html, Language.Java, Language.VBNET)]
[ProcessDependency<SourceMonitorProcessDependency>]
public class SourceMonitorMetricCalculator : IMetricCalculator
{
    private const string ProjectNameReplacement = "{{project_name}}";
    private const string ProjectDirectoryReplacement = "{{project_directory}}";
    private const string ProjectFileDirectoryReplacement = "{{project_file_directory}}";
    private const string ProjectLanguageReplacement = "{{project_language}}";
    private const string ReportsPathReplacement = "{{reports_path}}";

    private const string FileExtension = "xml";

    private readonly ILogger<SourceMonitorMetricCalculator> _logger;
    private readonly IProcessManager _processManager;
    private readonly string _xmlTemplate;


    private string _projectsPath;

    private string? _reportsPath;
    
    

    public SourceMonitorMetricCalculator(IProcessManager processManager,
        ILogger<SourceMonitorMetricCalculator> logger)
    {
        _processManager = processManager;
        _logger = logger;
        _xmlTemplate = File.ReadAllText(Resource.SourceMonitor.TemplateXml);
    }


    public async Task<IResult> CalculateMetricsAsync(RepositoryWithBranchNameDto repositoryWithBranchNameDto, string baseRepositoriesDirectoryPath = "",
        string baseReportsDirectoryPath = "", CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var repository = repositoryWithBranchNameDto.Repository;
        var branchName = repositoryWithBranchNameDto.BranchName;
        if (string.IsNullOrWhiteSpace(branchName)) branchName = repository.DefaultBranch;
        _reportsPath = string.IsNullOrEmpty(baseReportsDirectoryPath) ? PathHelper.TempPath : baseReportsDirectoryPath;
        _projectsPath = string.IsNullOrEmpty(baseRepositoriesDirectoryPath)
            ? PathHelper.BuildRepositoryDirectoryPath(PathHelper.TempPath, repository.Language, repository.FullName, branchName)
            : baseRepositoriesDirectoryPath;
        await ProcessRepositoryAsync(repository, branchName, cancellationToken);
        var reportsPath =
            PathHelper.BuildReportPath(_reportsPath, repository.Language, repository.FullName, branchName.Replace('/', '-'), FileExtension);

        if (!File.Exists(reportsPath))
        {
            _logger.LogError("SourceMonitor reports file not found");
            return new NullResult();
        }

        var xmlDocument = new XmlDocument();
        xmlDocument.Load(reportsPath);

        AddIdToXml(repository, xmlDocument, reportsPath);

        FileNameChange(repository, reportsPath);

        return new SourceMonitorResult(repository, GetMetrics(xmlDocument).Append(new Metric("BranchName", branchName)).ToList());
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

        return metrics;
    }
    
    private static void FileNameChange(Repository repository, FilePathString reportsPath)
    {
        // file name change
        var fileInfo = new FileInfo(reportsPath);
        var newFileName = $"id_{repository.Id}_{fileInfo.Name}";
        var newFilePath = reportsPath.ParentDirectory + newFileName;
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

    private async Task ProcessRepositoryAsync(Repository repository, string branchName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var reportPath =
            PathHelper.BuildReportPath(_reportsPath!, repository.Language, repository.FullName, branchName.Replace('/', '-'),FileExtension);
        if (File.Exists(reportPath))
        {
            _logger.LogInformation("Reports already exist for {RepositoryName}. Skipping...", repository.FullName);
            return;
        }

        await CalculateStatisticsUsingSourceMonitorAsync(repository, branchName, reportPath.ParentDirectory, cancellationToken);
    }

    private async Task CalculateStatisticsUsingSourceMonitorAsync(Repository repository, string branchName,
        DirectoryPathString workingDirectory,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Calculating statistics for {RepositoryName}", repository.FullName);
        var xmlPath = await CreateSourceMonitorXmlAsync(repository, branchName, cancellationToken);
        workingDirectory = xmlPath.ToFilePathString().ParentDirectory;
        var result =
            await _processManager.RunAsync(new ProcessStartInfo(Resource.SourceMonitor.SourceMonitorExe,
                $"/C \"{xmlPath}\"", workingDirectory), cancellationToken);
        _logger.LogDebug("SourceMonitor log: {SourceMonitorLog}", result.Output);
        _logger.LogError("SourceMonitor error log: {SourceMonitorErrorLog}", result.Error);
        if (result.ExitCode == 0)
            _logger.LogInformation("Statistics for {RepositoryName} calculated successfully", repository.FullName);
        else
            _logger.LogError("Error while calculating statistics for {RepositoryName}", repository.FullName);
    }

    private async Task<string> CreateSourceMonitorXmlAsync(Repository repository, string branchName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var xmlDirectory =
            PathHelper.BuildDirectoryPath(_reportsPath, repository.Language, "SourceMonitor", repository.Owner.Login);
        xmlDirectory.CreateIfNotExists();

        var reportsPath = PathHelper
            .BuildReportPath(_reportsPath!, repository.Language, repository.FullName, branchName.Replace('/', '-'),FileExtension).ParentDirectory;
        
        reportsPath.CreateIfNotExists();


        var projectName = $"{repository.Name}-{branchName.Replace('/', '-')}";
        var xmlPath = xmlDirectory + $"{projectName}.xml".ToFilePathString();

        xmlPath.DeleteIfExists();

        await ContentErrorHandleRepositoryAsync(_projectsPath, repository.Language, cancellationToken);

        var xml = _xmlTemplate
            .Replace(ProjectNameReplacement, projectName)
            .Replace(ProjectDirectoryReplacement, _projectsPath)
            .Replace(ProjectFileDirectoryReplacement, xmlDirectory)
            .Replace(ProjectLanguageReplacement, GitConsts.LanguagesMap[repository.Language].GetNormalizedLanguage())
            .Replace(ReportsPathReplacement, reportsPath);

        await File.WriteAllTextAsync(xmlPath, xml, cancellationToken);
        return xmlPath;
    }

    private async Task ContentErrorHandleRepositoryAsync(string path, string lang, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var language = GitConsts.LanguagesMap[lang];

        var handlerMethods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(m =>
        {
            var attribute = m.GetCustomAttribute<LanguageAttribute>();
            return attribute != null && attribute.Languages.Contains(language);
        });

        foreach (var handlerMethod in handlerMethods)
            try
            {
                if (typeof(Task).IsAssignableFrom(handlerMethod.ReturnType))
                    await (handlerMethod.Invoke(this, new object[] { path, cancellationToken }) as Task)!;
                else
                    handlerMethod.Invoke(this, new object[] { path });
            }
            catch
            {
                // ignored
            }
    }

    [Language(Language.CSharp)]
    private async Task SwitchExpressionHandleAsync(string path, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);

        foreach (var filePath in files)
            try
            {
                var regex = new Regex(@"\bswitch\s*\{[^}]+\}");
                var text = await File.ReadAllTextAsync(filePath, cancellationToken);
                var matches = regex.Matches(text);
                foreach (Match match in matches)

                {
                    var matchLineStart = text.LastIndexOf("\r\n", match.Index, StringComparison.Ordinal);
                    var matchLineEnd = text.IndexOf("\r\n", match.Index, StringComparison.Ordinal);
                    var matchLine = text.Substring(matchLineStart, matchLineEnd - matchLineStart);

                    var newValue = "/*" + matchLine + "*/";
                    text = text.Replace(matchLine, newValue);
                }

                await File.WriteAllTextAsync(filePath, text, cancellationToken);
            }
            catch
            {
                // ignored
            }
    }
}