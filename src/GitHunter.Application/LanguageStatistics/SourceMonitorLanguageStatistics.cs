using GitHunter.Application.Git;
using GitHunter.Application.Resources;
using GitHunter.Core.DependencyProcesses;
using GitHunter.Core.Helpers;
using GitHunter.Core.Processes;
using Microsoft.Extensions.Logging;
using Octokit;

namespace GitHunter.Application.LanguageStatistics;

[Language(Language.CSharp, Language.CPlusPlus, Language.Java)]
[ProcessDependency<SourceMonitorProcessDependency>]
public class SourceMonitorLanguageStatistics : ILanguageStatistics
{
    private const string ProjectNameReplacement = "{{project_name}}";
    private const string ProjectDirectoryReplacement = "{{project_directory}}";
    private const string ProjectFileDirectoryReplacement = "{{project_file_directory}}";
    private const string ProjectLanguageReplacement = "{{project_language}}";
    private const string ReportsPathReplacement = "{{reports_path}}";

    private readonly ILogger<SourceMonitorLanguageStatistics> _logger;
    private readonly IProcessManager _processManager;
    private readonly IGitManager _gitManager;


    public SourceMonitorLanguageStatistics(IGitManager gitManager, IProcessManager processManager,
        ILogger<SourceMonitorLanguageStatistics> logger)
    {
        _gitManager = gitManager;
        _processManager = processManager;
        _logger = logger;
    }


    public Task GetStatisticsAsync(Repository repository, CancellationToken token = default)
    {
        return ProcessRepository(repository, token);
    }

    private async Task ProcessRepository(Repository repository, CancellationToken token = default)
    {
        if (token.IsCancellationRequested)
            return;
        var reportsPath = Path.Combine(repository.Language, "Reports", repository.Name + ".xml");
        if (File.Exists(reportsPath))
        {
            _logger.LogInformation("Reports already exist for {RepositoryName}. Skipping...", repository.Name);
            return;
        }

        var isSuccess = await _gitManager.CloneRepository(repository, token);

        if (!isSuccess) return;

        await CalculateStatisticsUsingSourceMonitor(repository);
    }

    private async Task CalculateStatisticsUsingSourceMonitor(Repository repository)
    {
        _logger.LogInformation("Calculating statistics for {RepositoryName}", repository.Name);
        var xmlPath = await CreateSourceMonitorXml(repository);

        var result = await _processManager.RunAsync(Resource.SourceMonitor.SourceMonitorExe.Value, $"/C \"{xmlPath}\"");
        if (result.ExitCode == 0)
            _logger.LogInformation("Statistics for {RepositoryName} calculated successfully", repository.Name);
        else
            _logger.LogError("Error while calculating statistics for {RepositoryName}", repository.Name);
    }

    private async Task<string> CreateSourceMonitorXml(Repository repository)
    {
        var xmlDirectory = PathHelper.BuildFullPath(repository.Language, "SourceMonitor", repository.Name);

        var reportsPath = PathHelper.BuildFullPath(repository.Language, "Reports");

        var projectDirectory = PathHelper.BuildFullPath(repository.Language, "Repositories", repository.Name);

        var xmlPath = Path.Combine(xmlDirectory, $"{repository.Name}.xml");

        if (File.Exists(xmlPath))
        {
            _logger.LogInformation("SourceMonitor xml file already exists for {RepositoryName}. Skipping...",
                repository.Name);
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