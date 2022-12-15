using GitHunter.Application.Resources;
using GitHunter.Core.DependencyProcesses;
using GitHunter.Core.Helpers;
using GitHunter.Core.Processes;
using Microsoft.Extensions.Logging;
using Octokit;

namespace GitHunter.Application.LanguageStatistics.SourceMonitor;

[Language(Language.CSharp, Language.CPlusPlus, Language.Java)]
[ProcessDependency<SourceMonitorProcessDependency>]
public class SourceMonitorMetricCalculator : IMetricCalculator
{
    private const string ProjectNameReplacement = "{{project_name}}";
    private const string ProjectDirectoryReplacement = "{{project_directory}}";
    private const string ProjectFileDirectoryReplacement = "{{project_file_directory}}";
    private const string ProjectLanguageReplacement = "{{project_language}}";
    private const string ReportsPathReplacement = "{{reports_path}}";

    private readonly ILogger<SourceMonitorMetricCalculator> _logger;
    private readonly IProcessManager _processManager;


    public SourceMonitorMetricCalculator(IProcessManager processManager,
        ILogger<SourceMonitorMetricCalculator> logger)
    {
        _processManager = processManager;
        _logger = logger;
    }


    public Task CalculateMetricsAsync(Repository repository, CancellationToken token = default)
    {
        return ProcessRepository(repository, token);
    }

    private async Task ProcessRepository(Repository repository, CancellationToken token = default)
    {
        if (token.IsCancellationRequested)
            return;
        var reportsPath = Path.Combine(repository.Language, "Reports", repository.FullName + ".xml");
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

        var projectDirectory = PathHelper.BuildFullPath(repository.Language, "Repositories", repository.FullName);

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