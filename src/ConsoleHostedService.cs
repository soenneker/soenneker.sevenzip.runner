﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Soenneker.Compression.SevenZip.Abstract;
using Soenneker.GitHub.Repositories.Releases.Abstract;
using Soenneker.Managers.Runners.Abstract;
using Soenneker.Utils.Directory.Abstract;

namespace Soenneker.SevenZip.Runner;

public sealed class ConsoleHostedService : IHostedService
{
    private readonly ILogger<ConsoleHostedService> _logger;

    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IRunnersManager _runnersManager;
    private readonly IGitHubRepositoriesReleasesUtil _releasesUtil;
    private readonly IDirectoryUtil _directoryUtil;
    private readonly ISevenZipCompressionUtil _sevenZipUtil;

    private int? _exitCode;

    public ConsoleHostedService(ILogger<ConsoleHostedService> logger, IHostApplicationLifetime appLifetime, IRunnersManager runnersManager,
        IGitHubRepositoriesReleasesUtil releasesUtil, IDirectoryUtil directoryUtil, ISevenZipCompressionUtil sevenZipUtil)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _runnersManager = runnersManager;
        _releasesUtil = releasesUtil;
        _directoryUtil = directoryUtil;
        _sevenZipUtil = sevenZipUtil;
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                _logger.LogInformation("Running console hosted service ...");

                try
                {
                    string downloadDir = await _directoryUtil.CreateTempDirectory(cancellationToken);

                    string? asset = await _releasesUtil.DownloadReleaseAssetByNamePattern("ip7z", "7zip", downloadDir, ["extra.7z"], cancellationToken);

                    if (asset == null)
                        throw new FileNotFoundException("Could not find asset.");

                    string extractionDir = await _sevenZipUtil.ExtractAdvanced(asset, null, false, cancellationToken);

                    string finishedAssetPath = Path.Combine(extractionDir, "x64", Constants.FileName);

                    await _runnersManager.PushIfChangesNeeded(finishedAssetPath, Constants.FileName, Constants.Library,
                        $"https://github.com/soenneker/{Constants.Library}", cancellationToken);

                    _logger.LogInformation("Complete!");

                    _exitCode = 0;
                }
                catch (Exception e)
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();

                    _logger.LogError(e, "Unhandled exception");

                    await Task.Delay(2000, cancellationToken);
                    _exitCode = 1;
                }
                finally
                {
                    // Stop the application once the work is done
                    _appLifetime.StopApplication();
                }
            }, cancellationToken);
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Exiting with return code: {exitCode}", _exitCode);

        // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
        return Task.CompletedTask;
    }
}