using Microsoft.Extensions.DependencyInjection;
using Soenneker.Managers.Runners.Registrars;
using Soenneker.GitHub.Repositories.Releases.Registrars;
using Soenneker.Compression.SevenZip.Registrars;

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
                .AddGitHubRepositoriesReleasesUtilAsScoped()
                .AddRunnersManagerAsScoped()
                .AddSevenZipCompressionUtilAsScoped();

        return services;
    }
}