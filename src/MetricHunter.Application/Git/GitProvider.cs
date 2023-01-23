using AdvancedPath;
using MetricHunter.Core.DependencyProcesses;
using MetricHunter.Core.Jsons;
using MetricHunter.Core.Paths;
using MetricHunter.Core.Processes;
using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Application.Git;

[ProcessDependency<GitProcessDependency>]
public class GitProvider : IGitProvider, ISingletonDependency
{
    private readonly ILogger<GitProvider> _logger;
    private readonly IProcessManager _processManager;

    public GitProvider(IProcessManager processManager, ILogger<GitProvider> logger)
    {
        _processManager = processManager;
        _logger = logger;
    }

    public async Task<bool> CloneRepositoryAsync(Repository repository, string cloneBaseDirectoryPath = "",
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(cloneBaseDirectoryPath)) cloneBaseDirectoryPath = PathHelper.TempPath;
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            _logger.LogInformation($"Cloning {repository.FullName}...");

            var repositoryPath =
                PathHelper.BuildRepositoryDirectoryPath(cloneBaseDirectoryPath, repository.Language,
                    repository.FullName);

            var path = repositoryPath.ParentDirectory;
            if (repositoryPath.Exists)
            {
                if (!(repositoryPath + GitConsts.RepositoryInfoFileExtension).Exists)
                {
                    await DeleteLocalRepositoryAsync(repositoryPath, cancellationToken);
                }
                else
                {
                    _logger.LogInformation($"Repository {repository.FullName} already exists.");
                    _logger.LogInformation($"Updating {repository.FullName}...");

                    var r = await _processManager.RunAsync(new ProcessStartInfo("git",
                        $"pull {repository.CloneUrl} --allow-unrelated-histories",
                        repositoryPath), cancellationToken);
                    if (r.ExitCode != 0)
                    {
                        _logger.LogWarning(
                            $"Repository {repository.FullName} is not a git repository. Deleting and cloning again...");
                        await DeleteLocalRepositoryAsync(repositoryPath, cancellationToken);
                    }
                    else
                    {
                        _logger.LogInformation($"Repository {repository.FullName} updated.");
                        return true;
                    }
                }
            }

            try
            {
                var result =
                    await _processManager.RunAsync(new ProcessStartInfo("git",
                        $"clone -c core.longpaths=true {repository.CloneUrl}", path), cancellationToken);
                if (result.ExitCode == 0)
                    _logger.LogInformation($"Cloned {repository.FullName} successfully.");
                else
                    _logger.LogError($"Failed to clone {repository.FullName}, exit code: {result.ExitCode}");

                if (result.ExitCode == 128)
                {
                    await Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);
                    result =
                        await _processManager.RunAsync(new ProcessStartInfo("git",
                            $"clone -c core.longpaths=true {repository.CloneUrl}", path), cancellationToken);
                    if (result.ExitCode == 0)
                        _logger.LogInformation($"Cloned {repository.FullName} successfully.");
                    else
                        _logger.LogError($"Failed to clone {repository.FullName}, exit code: {result.ExitCode}");
                }

                if (result.ExitCode == 0)
                {
                    AddRepositoryInfoFile(cloneBaseDirectoryPath, repository);
                    OnCloneRepositorySuccess(new CloneRepositorySuccessEventArgs(repository, repositoryPath));
                }

                else
                {
                    OnCloneRepositoryError(new CloneRepositoryErrorEventArgs(repository, null));
                }

                return result.ExitCode == 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to clone {repository.FullName}");
                OnCloneRepositoryError(new CloneRepositoryErrorEventArgs(repository, e));
                return false;
            }
        }
        catch (Exception e)
        {
            OnCloneRepositoryError(new CloneRepositoryErrorEventArgs(repository, e));
            return false;
        }
    }

    public event EventHandler<CloneRepositoryErrorEventArgs>? CloneRepositoryError;
    public event EventHandler<CloneRepositorySuccessEventArgs>? CloneRepositorySuccess;


    public Task<bool> DeleteLocalRepositoryAsync(Repository repository, string cloneBaseDirectoryPath = "",
        CancellationToken token = default)
    {
        if (string.IsNullOrWhiteSpace(cloneBaseDirectoryPath)) cloneBaseDirectoryPath = PathHelper.TempPath;
        var repositoryPath =
            PathHelper.BuildRepositoryDirectoryPath(cloneBaseDirectoryPath, repository.Language, repository.FullName);

        return DeleteLocalRepositoryAsync(repositoryPath, token);
    }

    public Task<bool> DeleteLocalRepositoryAsync(string path, CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();

        if (Directory.Exists(path))
        {
            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            files.AsParallel().ForAll(f =>
            {
                try
                {
                    File.SetAttributes(f, FileAttributes.Normal);
                    File.Delete(f);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Failed to delete {f}");
                }
            });

            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to delete {path}");
            }
        }

        return Task.FromResult(true);
    }

    protected virtual void OnCloneRepositoryError(CloneRepositoryErrorEventArgs e)
    {
        CloneRepositoryError?.Invoke(this, e);
    }

    protected virtual void OnCloneRepositorySuccess(CloneRepositorySuccessEventArgs e)
    {
        CloneRepositorySuccess?.Invoke(this, e);
    }

    private async void AddRepositoryInfoFile(DirectoryPathString repositoryPath, Repository repository)
    {
        var repositoryInfoFilePath = (repositoryPath + GitConsts.RepositoryInfoFileExtension).ToFilePathString();
        await JsonHelper.AppendJsonAsync(repository, repositoryInfoFilePath, r => r.Id);
    }
}