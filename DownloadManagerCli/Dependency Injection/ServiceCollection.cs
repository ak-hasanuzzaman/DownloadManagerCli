using DownloadManagerCli.Engine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManagerCli.Dependency_Injection
{
    internal static class Dependency
    {
        static IConfiguration Configuration;
        public static IServiceCollection ConfigureServices()
        {
            Configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();

            //var cosmosDBConfig = new CosmosDBConfig();
            //Configuration.GetSection(nameof(DBName.CosmosDb)).Bind(cosmosDBConfig);

            //var rabbitMqConfig = new RabbitMqConfig();
            //Configuration.GetSection(nameof(ConfigSection.RabbitMq)).Bind(rabbitMqConfig);

            var services = new ServiceCollection();

            //services.AddScoped<IDbService, CosmosDbService>();
            //services.AddTransient<IReductionCalculation, ReductionCalculation>();
            services.AddScoped<DownloadFile>();

            //services.AddSingleton(RabbitHutch.CreateBus(rabbitMqConfig.ConnectionString));

            //services.AddSingleton(new CosmosClient(cosmosDBConfig.Account, cosmosDBConfig.Key));
            //services.Configure<CosmosDBConfig>(Configuration.GetSection(nameof(DBName.CosmosDb)));

            return services;
        }

    }
}
