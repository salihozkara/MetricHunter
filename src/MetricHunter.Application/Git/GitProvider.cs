using MetricHunter.Core.DependencyProcesses;
using MetricHunter.Core.Paths;
using MetricHunter.Core.Processes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    
    public async Task<bool> CloneRepository(Repository repository, string cloneBaseDirectoryPath = "", CancellationToken token = default)
    {
        if(string.IsNullOrWhiteSpace(cloneBaseDirectoryPath))
        {
            cloneBaseDirectoryPath = PathHelper.TempPath;
        }
        try
        {
            if (token.IsCancellationRequested) return false;

            _logger.LogInformation($"Cloning {repository.FullName}...");

            var repositoryPath = PathHelper.BuildRepositoryDirectoryPath(cloneBaseDirectoryPath, repository.Language, repository.FullName);

            var path = Directory.GetParent(repositoryPath)?.FullName;
            if (Directory.Exists(repositoryPath))
            {
                if (!File.Exists(Path.Combine(repositoryPath, GitConsts.RepositoryInfoFileExtension)))
                    await DeleteLocalRepository(repositoryPath, token);
                else
                {
                    _logger.LogInformation($"Repository {repository.FullName} already exists.");
                    _logger.LogInformation($"Updating {repository.FullName}...");

                    var r = await _processManager.RunAsync(new ProcessStartInfo("git",
                        $"pull {repository.CloneUrl} --allow-unrelated-histories",
                        repositoryPath));
                    if (r.ExitCode != 0)
                    {
                        _logger.LogWarning(
                            $"Repository {repository.FullName} is not a git repository. Deleting and cloning again...");
                        await DeleteLocalRepository(repositoryPath, token);
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
                        $"clone -c core.longpaths=true {repository.CloneUrl}", path));
                if (result.ExitCode == 0)
                    _logger.LogInformation($"Cloned {repository.FullName} successfully.");
                else
                    _logger.LogError($"Failed to clone {repository.FullName}, exit code: {result.ExitCode}");

                if (result.ExitCode == 128)
                {
                    await Task.Delay(TimeSpan.FromSeconds(15), token);
                    result =
                        await _processManager.RunAsync(new ProcessStartInfo("git",
                            $"clone -c core.longpaths=true {repository.CloneUrl}", path));
                    if (result.ExitCode == 0)
                        _logger.LogInformation($"Cloned {repository.FullName} successfully.");
                    else
                        _logger.LogError($"Failed to clone {repository.FullName}, exit code: {result.ExitCode}");
                }

                if (result.ExitCode == 0)
                    OnCloneRepositorySuccess(new CloneRepositorySuccessEventArgs(repository, repositoryPath));
                else
                    OnCloneRepositoryError(new CloneRepositoryErrorEventArgs(repository, null));

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


    public Task<bool> DeleteLocalRepository(Repository repository, string cloneBaseDirectoryPath = "", CancellationToken token = default)
    {
        if(string.IsNullOrWhiteSpace(cloneBaseDirectoryPath))
        {
            cloneBaseDirectoryPath = PathHelper.TempPath;
        }
        var repositoryPath = PathHelper.BuildRepositoryDirectoryPath(cloneBaseDirectoryPath, repository.Language, repository.FullName);

        return DeleteLocalRepository(repositoryPath, token);
    }

    public Task<bool> DeleteLocalRepository(string path, CancellationToken token = default)
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
        AddRepositoryInfoFile(e.LocalPath, e.Repository);
        CloneRepositorySuccess?.Invoke(this, e);
    }
    
    private void AddRepositoryInfoFile(string repositoryPath, Repository repository)
    {
        var repositoryInfoFilePath = Path.Combine(repositoryPath, GitConsts.RepositoryInfoFileExtension)!;
        Directory.CreateDirectory(Path.GetDirectoryName(repositoryInfoFilePath)!);
        File.WriteAllText(repositoryInfoFilePath, JsonConvert.SerializeObject(repository));
    }
}