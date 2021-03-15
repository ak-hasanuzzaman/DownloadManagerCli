namespace DownloadManagerCli.Engine
{
    using DownloadManagerCli.Abstraction.Interfaces;
    using DownloadManagerCli.Model.DownloadSource;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    public class DownloadSourceFile : IDownloadSourceFile
    {
        readonly IRemoteServerCall _remoteServerCall;
        readonly IContentWriter _contentWriter;
        public DownloadSourceFile(IRemoteServerCall remoteServerCall, IContentWriter contentWriter)
        {
            _remoteServerCall = remoteServerCall;
            _contentWriter = contentWriter;
        }
        public async Task DownloadAsync(Source downloadSource)
        {
            if (!Directory.Exists(downloadSource.Config.DownloadDir))
                Directory.CreateDirectory(downloadSource.Config.DownloadDir);

            await ProcessParallelDownloadsAsync(downloadSource);
        }
        public void ExecuteDryRun(Model.DownloadSource.Download[] downloads)
        {
            Console.WriteLine("A dry-run shows that the following: ");
            Console.WriteLine();
            foreach (var d in downloads)
            {
                var s = $"Url : {d.Url}{Environment.NewLine}File: {d.File}{Environment.NewLine}Sha256: {d.Sha256}{Environment.NewLine}Sha1: {d.Sha1}{Environment.NewLine}Overwrite: {d.Overwrite}";
                Console.WriteLine(s);
                Console.WriteLine();
            }
        }
        private async Task ProcessParallelDownloadsAsync(Source downloadSource)
        {
            int count = 0;

            for (var i = 0; i < downloadSource.Downloads.Length; i = i + downloadSource.Config.ParallelDownloads)
            {
                var items = downloadSource.Downloads.Skip(i).Take(downloadSource.Config.ParallelDownloads);

                foreach (var item in items)
                {
                    count = count + 1;

                    Console.WriteLine($"File # {count} of {downloadSource.Downloads.Length} File name : {item.File.ToUpper()} is downloading");

                    var downloadToFilePath = Path.Combine(downloadSource.Config.DownloadDir, item.File);
                    if (!Path.HasExtension(downloadToFilePath))
                    {
                        Console.WriteLine($"File name : {item.File.ToUpper()} does not have an extension");
                        continue;
                    }

                    var fileBytes = await _remoteServerCall.GetBytesAsync(item.Url);
                    await _contentWriter.WriteToDiskAsync(downloadToFilePath, fileBytes);

                    Console.WriteLine($"File {item.File} downloaded successfully");

                }
            }
        }
    }
}
