using GitHunter.Core.DependencyProcesses;
using GitHunter.Core.Helpers;
using GitHunter.Core.Processes;
using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace GitHunter.Application.Git;

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

    // TODO: Add path to parameters
    public async Task<bool> CloneRepository(Repository repository, CancellationToken token = default)
    {
        try
        {
            if (token.IsCancellationRequested) return false;

            _logger.LogInformation($"Cloning {repository.FullName}...");
            var path = PathHelper.BuildAndCreateFullPath(repository.Language, "Repositories", repository.Owner.Login);

            var repositoryPath = Path.Combine(path, repository.Name);
            if (Directory.Exists(repositoryPath))
            {
                var r = await _processManager.RunAsync("git", $"pull {repository.CloneUrl} --allow-unrelated-histories",
                    path);
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

            var result =
                await _processManager.RunAsync("git", $"clone -c core.longpaths=true {repository.CloneUrl}", path);
            if (result.ExitCode == 0)
                _logger.LogInformation($"Cloned {repository.FullName} successfully.");
            else
                _logger.LogError($"Failed to clone {repository.FullName}.");

            if (result.ExitCode == 0)
                OnCloneRepositorySuccess(new CloneRepositorySuccessEventArgs(repository));
            else
                OnCloneRepositoryError(new CloneRepositoryErrorEventArgs(repository, null));

            return result.ExitCode == 0;
        }
        catch (Exception e)
        {
            OnCloneRepositoryError(new CloneRepositoryErrorEventArgs(repository, e));
            return false;
        }
    }

    public event EventHandler<CloneRepositoryErrorEventArgs>? CloneRepositoryError;
    public event EventHandler<CloneRepositorySuccessEventArgs>? CloneRepositorySuccess;


    public Task<bool> DeleteLocalRepository(Repository repository, CancellationToken token = default)
    {
        var path = PathHelper.BuildAndCreateFullPath(repository.Language, "Repositories", repository.Owner.Login);

        var repositoryPath = Path.Combine(path, repository.Name);

        return DeleteLocalRepository(repositoryPath, token);
    }

    public Task<bool> DeleteLocalRepository(string path, CancellationToken token = default)
    {
        if (token.IsCancellationRequested) return Task.FromResult(false);

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
}