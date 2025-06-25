using Microsoft.Extensions.DependencyInjection;
using Soenneker.Git.Util.Registrars;
using Soenneker.SevenZip.Runner.Utils;
using Soenneker.SevenZip.Runner.Utils.Abstract;
using Soenneker.Managers.Runners.Registrars;
using Soenneker.Utils.File.Download.Registrars;
using Soenneker.GitHub.Repositories.Releases.Registrars;

namespace Soenneker.SevenZip.Runner;

/// <summary>
/// Console type startup
/// </summary>
public static class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    public static void ConfigureServices(IServiceCollection services)
    {
        services.SetupIoC();
    }

    public static IServiceCollection SetupIoC(this IServiceCollection services)
    {
        services.AddHostedService<ConsoleHostedService>()
                .AddScoped<IFileOperationsUtil, FileOperationsUtil>()
                .AddFileDownloadUtilAsScoped()
                .AddGitHubRepositoriesReleasesUtilAsScoped()
                .AddRunnersManagerAsScoped();

        return services;
    }
}
