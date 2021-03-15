namespace DownloadManagerCli
{
    using CommandLine;
    using DownloadManagerCli.Abstraction.AbstractBase;
    using DownloadManagerCli.Dependency_Injection;
    using DownloadManagerCli.Engine;
    using DownloadManagerCli.Model.CliArgumentOptions;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    partial class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var services = Dependency.ConfigureServices();
                var serviceProvider = services.BuildServiceProvider();
                var downloadFile = serviceProvider.GetRequiredService<DownloadSourceFile>();

                #region MyRegion
                //var yml = @"
                //config:
                //    parallel_downloads: 3
                //    download_dir: ./downloads/linux-images
                //downloads:
                //     - url: https://releases.ubuntu.com/20.04.2/ubuntu-20.04.2-live-server-amd64.iso
                //       file: ubuntu-20.04.2-live-server-amd64.iso
                //       sha256: d1f2bf834bbe9bb43faf16f9be992a6f3935e65be0edece1dee2aa6eb1767423
                //       overwrite: true
                //     - url: https://releases.ubuntu.com/20.04.2/ubuntu-20.04.2-live-server-amd64.iso
                //       file: ubuntu-20.04.2-live-server-amd64.iso
                //       sha256: d1f2bf834bbe9bb43faf16f9be992a6f3935e65be0edece1dee2aa6eb1767423
                //       overwrite: true
                //     - url: https://releases.ubuntu.com/20.04.2/ubuntu-20.04.2-live-server-amd64.iso
                //       file: ubuntu-20.04.2-live-server-amd64.iso
                //       sha256: d1f2bf834bbe9bb43faf16f9be992a6f3935e65be0edece1dee2aa6eb1767423
                //       overwrite: true
                //     - url: https://releases.ubuntu.com/20.04.2/ubuntu-20.04.2-live-server-amd64.iso
                //       file: ubuntu-20.04.2-live-server-amd64.iso
                //       sha256: d1f2bf834bbe9bb43faf16f9be992a6f3935e65be0edece1dee2aa6eb1767423
                //       overwrite: true
                //     - url: https://releases.ubuntu.com/20.04.2/ubuntu-20.04.2-live-server-amd64.iso
                //       file: ubuntu-20.04.2-live-server-amd64.iso
                //       sha256: d1f2bf834bbe9bb43faf16f9be992a6f3935e65be0edece1dee2aa6eb1767423
                //       overwrite: true
                //    ";

                //var deserializer = new DeserializerBuilder()
                //    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                //    .Build();

                //var p = deserializer.Deserialize<DownloadSource>(yml); 
                #endregion

                await StartToDownload(args, downloadFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();


            Console.ReadLine();
        }

        public static async Task StartToDownload(string[] args, DownloadSourceFile downloadFile)
        {
            Log.Logger.Information("Cli started to download");

            var types = ReadSourceFileUsingArguments.LoadVerbs();

            DownloadOptions downloadOptions = new DownloadOptions();

            Parser.Default.ParseArguments(args, types?.ToArray())
                .WithParsed<DownloadOptions>(async options =>
              {
                  downloadOptions = options;

                  ReadSourceFileUsingArguments.ValidateArguments(downloadOptions);
                  DownloadSourceBase downloadSourceBase = ReadSourceFileUsingArguments.GetDownloadSource(downloadOptions);
                  var source = downloadSourceBase
                                  .GetDownloadSourceFromFile(downloadOptions.FilePath);

                  ReadSourceFileUsingArguments.ValidateSourceObject(source);

                  if (downloadOptions.DryRun)
                  {
                      if (source == null || source.Downloads == null)
                          throw new Exception("Invalid source file");

                      downloadFile.ExecuteDryRun(source.Downloads);
                  }

                  if (downloadOptions.Download)
                  {
                      await downloadFile.DownloadAsync(source);
                  }
              })
                .WithNotParsed(ReadSourceFileUsingArguments.HandleParseError);

            Log.Logger.Information("Download completed");

            await Task.FromResult(0);
        }
    }
}
