namespace DownloadManagerCli
{
    using CommandLine;
    using DownloadManagerCli.Abstraction.AbstractBase;
    using DownloadManagerCli.Abstraction.AbstractClasses;
    using DownloadManagerCli.Dependency_Injection;
    using DownloadManagerCli.Engine;
    using DownloadManagerCli.Engine.DownloadFiles;
    using DownloadManagerCli.Engine.ValidateInputSource;
    using DownloadManagerCli.Model.CliArgumentOptions;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    internal static class FileDownloadManager
    {
        private static ServiceProvider serviceProvider = null;
        public static async Task StartToDownload(string[] args)
        {
            var services = Dependency.ConfigureServices();
            serviceProvider = services.BuildServiceProvider();

            Log.Logger.Information("Cli started to download");

            var types = LoadVerbs();

            DownloadOptions downloadOptions = null;

            Parser.Default.ParseArguments(args, types?.ToArray())
                .WithParsed<DownloadOptions>(options =>
                {
                    downloadOptions = options;
                })
                .WithParsed<ValidateOptions>(options =>
                {
                    ExecuteValidateCommand(options);
                })
                .WithNotParsed(HandleParseError);

            if (downloadOptions != null)
                await ExecuteDownloadCommandAsync(downloadOptions)
                            .ConfigureAwait(false);

            Log.Logger.Information("Download completed");

        }

        private static async Task ExecuteDownloadCommandAsync(DownloadOptions downloadOptions)
        {
            var downloadFile = serviceProvider.GetRequiredService<DownloadSourceFile>();

            DownloadSourceBase downloadSourceBase
                               = GetDownloadSource(downloadOptions.FilePath);
            var fileSource = downloadSourceBase.GetSourceObjectFromFile(downloadOptions.FilePath);

            if (fileSource == null || fileSource.Downloads == null)
                throw new Exception("Invalid source file");

            fileSource.Verbose = downloadOptions.Verbose;
            fileSource.Config.ParallelDownloads = downloadOptions.NumberOfParallelDownloads > 0 ?
                                                        downloadOptions.NumberOfParallelDownloads :
                                                        fileSource.Config.ParallelDownloads;

            if (downloadOptions.DryRun)
            {
                downloadFile.ExecuteDryRun(fileSource.Verbose, fileSource.Downloads);
            }

            if (downloadOptions.Download)
            {
                await downloadFile.DownloadAsync(fileSource)
                                        .ConfigureAwait(false);
                Console.ReadLine();
            }
        }

        private static void ExecuteValidateCommand(ValidateOptions options)
        {
            var validateSource = serviceProvider.GetRequiredService<ValidateSource>();

            if (options.Verbose)
            {
                Console.WriteLine($"Input source validation details {Environment.NewLine}");
                validateSource.Validate(options.FilePath);
            }
            else
            {
                Console.WriteLine("Input file is valid");
            }
        }

        private static DownloadSourceBase GetDownloadSource(string sourcePath)
        {
            DownloadSourceFactory downloadSourceFactory = new ConcreteDownloadSourceFactory();
            var fileExtension = Path.GetExtension(sourcePath).Replace(".", "").ToUpper();
            DownloadSourceBase downloadSourceBase = downloadSourceFactory
                                                        .GetDownloadSource(fileExtension);
            return downloadSourceBase;
        }

        private static IEnumerable<Type> LoadVerbs()
                => from assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                   let assembly = Assembly.Load(assemblyName)
                   from type in assembly.GetTypes()
                    .Where(t => t.GetCustomAttribute<VerbAttribute>() != null)
                    .ToArray()
                   select type;

        private static void HandleParseError(IEnumerable<Error> errs)
            => Console.WriteLine("Provided arguments can't be parsed successfully");

    }
}
