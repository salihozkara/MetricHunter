using System.Text;
using AdvancedPath;
using MetricHunter.Application.Repositories;
using MetricHunter.Core.DependencyProcesses;
using MetricHunter.Core.Jsons;
using MetricHunter.Core.Paths;
using MetricHunter.Core.Processes;
using Microsoft.Extensions.Logging;
using Octokit;
using Volo.Abp.DependencyInjection;

namespace MetricHunter.Application.Git;

// TODO: Refactor this class
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
    
    public async Task<bool> CloneRepositoryAsync(RepositoryWithBranchNameDto repositoryWithBranchNameDto, string cloneBaseDirectoryPath = "", CancellationToken cancellationToken = default)
    {
        var @return = false;
        var repository = repositoryWithBranchNameDto.Repository;
        var branchName = repositoryWithBranchNameDto.BranchName;
        Exception? exception = null;
        DirectoryPathString repositoryPath = string.Empty;
        if (string.IsNullOrWhiteSpace(cloneBaseDirectoryPath)) cloneBaseDirectoryPath = PathHelper.TempPath;
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            _logger.LogInformation($"Cloning {repository.FullName} branch {branchName}...");

            repositoryPath = PathHelper.BuildRepositoryDirectoryPath(cloneBaseDirectoryPath, repository.Language, repository.FullName, branchName);
            
            var defaultBranchPath = PathHelper.BuildRepositoryDirectoryPath(cloneBaseDirectoryPath, repository.Language, repository.FullName, repository.DefaultBranch);

            var path = repositoryPath.ParentDirectory;
            if (repositoryPath.Exists)
            {
                if (await LocalRepositoryCheck(repository, branchName, repositoryPath, cancellationToken))
                {
                    @return = true;
                    return true;
                }
                await DeleteLocalRepositoryAsync(repositoryPath, cancellationToken);
            }
            if (defaultBranchPath.Exists)
            {
                if (await LocalRepositoryCheck(repository, repository.DefaultBranch, defaultBranchPath, cancellationToken))
                {
                    @return = await CreateNewLocalRepositoryWithBranchName(repositoryWithBranchNameDto, path, defaultBranchPath, repositoryPath, cloneBaseDirectoryPath);
                    return @return;
                }

                await DeleteLocalRepositoryAsync(defaultBranchPath, cancellationToken);
            }

            try
            {
                var args = new StringBuilder();
                args.Append($"clone -c core.longpaths=true {repository.CloneUrl}");
                
                args.Append($" {repository.DefaultBranch} -b {repository.DefaultBranch}");
                
                var argsString = args.ToString();
                
                var result =
                    await _processManager.RunAsync(new ProcessStartInfo("git",
                        argsString, path), cancellationToken);
                if (result.ExitCode == 0)
                    _logger.LogInformation($"Cloned {repository.FullName} successfully.");
                else
                    _logger.LogError($"Failed to clone {repository.FullName}, exit code: {result.ExitCode}");

                if (result.ExitCode == 128)
                {
                    await Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);
                    result =
                        await _processManager.RunAsync(new ProcessStartInfo("git",
                            argsString, path), cancellationToken);
                    if (result.ExitCode == 0)
                        _logger.LogInformation($"Cloned {repository.FullName} successfully.");
                    else
                        _logger.LogError($"Failed to clone {repository.FullName}, exit code: {result.ExitCode}");
                }

                if (result.ExitCode == 0)
                {
                    AddRepositoryInfoFile(defaultBranchPath.ParentDirectory, new RepositoryWithBranchNameDto(repository), cloneBaseDirectoryPath);
                    @return = await CreateNewLocalRepositoryWithBranchName(repositoryWithBranchNameDto, path, defaultBranchPath, repositoryPath, cloneBaseDirectoryPath);
                    return @return;
                }

                @return = false;
                return @return;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to clone {repository.FullName}");
                exception = e;
                @return = false;
                return @return;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to clone {repository.FullName}");
            exception = e;
            @return = false;
            return @return;
        }
        finally
        {
            if (@return)
            {
                OnCloneRepositorySuccess(new CloneRepositorySuccessEventArgs(repository, repositoryPath));
            }
            else
            {
                OnCloneRepositoryError(new CloneRepositoryErrorEventArgs(repository, exception));
            }
        }
    }

    private async Task<bool> CreateNewLocalRepositoryWithBranchName(RepositoryWithBranchNameDto repositoryWithBranchNameDto, DirectoryPathString path,
        DirectoryPathString defaultBranchPath, DirectoryPathString repositoryPath, string cloneBaseDirectoryPath)
    {
        var repository = repositoryWithBranchNameDto.Repository;
        var branchName = repositoryWithBranchNameDto.BranchName;
        if (branchName != repository.DefaultBranch)
        {
            await CloneRepositoryOtherBranchAsync(defaultBranchPath, repositoryPath);
            if(!await ChangeBranchAsync(repositoryPath, branchName)) return false;
            AddRepositoryInfoFile(path, repositoryWithBranchNameDto, cloneBaseDirectoryPath);
        }

        OnCloneRepositorySuccess(new CloneRepositorySuccessEventArgs(repository, repositoryPath));
        return true;
    }

    private async Task<bool> LocalRepositoryCheck(Repository repository, string branchName,
        DirectoryPathString repositoryPath, CancellationToken cancellationToken)
    {
        if (!(repositoryPath + GitConsts.RepositoryInfoFileExtension).Exists)
        {
            return false;
        }

        _logger.LogInformation($"Repository {repository.FullName} branch {branchName} exists. Updating...");

        var args = new StringBuilder();
        args.Append($"pull origin {branchName}");

        var r = await _processManager.RunAsync(new ProcessStartInfo("git",
            args.ToString(),
            repositoryPath), cancellationToken);

        if (r.ExitCode != 0)
        {
            _logger.LogWarning(
                $"Repository {repository.FullName} branch {branchName} update failed, exit code: {r.ExitCode}");
            return false;
        }

        _logger.LogInformation($"Repository {repository.FullName} branch {branchName} updated successfully.");
        return true;
    }

    public event EventHandler<CloneRepositoryErrorEventArgs>? CloneRepositoryError;
    public event EventHandler<CloneRepositorySuccessEventArgs>? CloneRepositorySuccess;

    private async Task CloneRepositoryOtherBranchAsync(DirectoryPathString path, DirectoryPathString repositoryPath)
    {
        await FolderCopyAsync(path, repositoryPath);
    }
    
    private async Task<bool> ChangeBranchAsync(DirectoryPathString path, string branchName)
    {
        // git reset --hard
        var resetResult = await _processManager.RunAsync(new ProcessStartInfo("git",
            $"reset --hard", path), CancellationToken.None);

        if (resetResult.ExitCode == 0)
        {
            _logger.LogInformation($"Reset repository successfully.");
        }
        
        // git clean -fxd
        var cleanResult = await _processManager.RunAsync(new ProcessStartInfo("git",
            $"clean -fxd", path), CancellationToken.None);
        
        if (cleanResult.ExitCode == 0)
        {
            _logger.LogInformation($"Clean repository successfully.");
        }
        
        // git checkout {branchName}
        var result = await _processManager.RunAsync(new ProcessStartInfo("git",
            $"checkout {branchName}", path), CancellationToken.None);
        if (result.ExitCode == 0)
            _logger.LogInformation($"Changed branch to {branchName} successfully.");
        else
            _logger.LogError($"Failed to change branch to {branchName}, exit code: {result.ExitCode}");
        
        return result.ExitCode == 0;
    }
    
    private Task FolderCopyAsync(string source, string destination)
    {
        return Task.Run(async () =>
        {
            var sourceDirectory = new DirectoryInfo(source);
            var destinationDirectory = new DirectoryInfo(destination);
            var taskList = new List<Task>();
            if (!sourceDirectory.Exists)
            {
                throw new DirectoryNotFoundException($"Source directory {source} does not exist.");
            }

            if (!destinationDirectory.Exists)
            {
                destinationDirectory.Create();
            }

            foreach (var file in sourceDirectory.GetFiles())
            {
                taskList.Add(FileCopyAsync(file.FullName, Path.Combine(destinationDirectory.FullName, file.Name)));
            }
            
            await Task.WhenAll(taskList);
            
            taskList.Clear();

            foreach (var directory in sourceDirectory.GetDirectories())
            {
                taskList.Add(FolderCopyAsync(directory.FullName, Path.Combine(destinationDirectory.FullName, directory.Name)));
            }
            
            await Task.WhenAll(taskList);
        });
    }
    
    private static Task FileCopyAsync(string source, string destination)
    {
        return Task.Run(() =>
        {
            var sourceFile = new FileInfo(source);
            var destinationFile = new FileInfo(destination);
            if (!sourceFile.Exists)
            {
                throw new FileNotFoundException($"Source file {source} does not exist.");
            }

            if (destinationFile.Directory?.Exists == false)
            {
                destinationFile.Directory.Create();
            }

            File.Copy(source, destination, true);
        });
    }


    public Task<bool> DeleteLocalRepositoryAsync(RepositoryWithBranchNameDto repositoryWithBranchNameDto, string cloneBaseDirectoryPath = "", CancellationToken token = default)
    {
        if (string.IsNullOrWhiteSpace(cloneBaseDirectoryPath)) cloneBaseDirectoryPath = PathHelper.TempPath;
        var repositoryPath =
            PathHelper.BuildRepositoryDirectoryPath(cloneBaseDirectoryPath, repositoryWithBranchNameDto.Repository.Language, repositoryWithBranchNameDto.Repository.FullName, repositoryWithBranchNameDto.BranchName);

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

    private static async void AddRepositoryInfoFile(PathString repositoryPath, RepositoryWithBranchNameDto repositoryWithBranchNameDto, string cloneBaseDirectoryPath)
    {
        var repositoryInfoFilePath = (repositoryPath + repositoryWithBranchNameDto.BranchName + GitConsts.RepositoryInfoFileExtension).ToFilePathString();
        while (repositoryInfoFilePath >= cloneBaseDirectoryPath)
        {
            await JsonHelper.AppendJsonAsync(repositoryWithBranchNameDto, repositoryInfoFilePath, x => x.ToString());
            repositoryInfoFilePath = repositoryInfoFilePath.ParentDirectory.ParentDirectory + GitConsts.RepositoryInfoFileExtension.ToFilePathString();
        }
    }
}