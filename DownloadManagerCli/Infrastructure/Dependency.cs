namespace DownloadManagerCli.Dependency_Injection
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using System.IO;
    internal static class Dependency
    {
        static IConfiguration Configuration;
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            Configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();

            services.AddApplicationServices(Configuration);

            Log.Logger = new LoggerConfiguration()
                             .ReadFrom.Configuration(Configuration)
                             .CreateLogger();

            return services;
        }

    }
}
