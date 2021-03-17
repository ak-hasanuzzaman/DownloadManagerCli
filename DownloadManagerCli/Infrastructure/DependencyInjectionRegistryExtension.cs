namespace DownloadManagerCli.Dependency_Injection
{
    using DownloadManagerCli.Abstraction.Interfaces;
    using DownloadManagerCli.Engine.DownloadFiles;
    using DownloadManagerCli.Engine.ValidateInputSource;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;

    internal static class DependencyInjectionRegistryExtension
    {
        public static void AddApplicationServices(this ServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Logging"));
                builder.AddConsole();
                builder.AddSerilog();
            });

            services.AddSingleton<DownloadSourceFile>();
            services.AddSingleton<ValidateSource>();

            services.AddSingleton<IRemoteServerCall, RemoteServerCall>();
            //services.AddSingleton<IContentWriter, ContentWriter>();
        }
    }
}
