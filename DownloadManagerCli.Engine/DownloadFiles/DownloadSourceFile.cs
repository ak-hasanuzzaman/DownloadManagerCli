namespace DownloadManagerCli.Engine.DownloadFiles
{
    using DownloadManagerCli.Abstraction.Interfaces;
    using DownloadManagerCli.Model.DownloadSource;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    public class DownloadSourceFile : IDownloadSourceFile
    {
        readonly IRemoteServerCall _remoteServerCall;
        private static int Count;

        public DownloadSourceFile(IRemoteServerCall remoteServerCall)
        {
            _remoteServerCall = remoteServerCall;
        }
        public async Task DownloadAsync(Source downloadSource)
        {
            if (!Directory.Exists(downloadSource.Config.DownloadDirectory))
                Directory.CreateDirectory(downloadSource.Config.DownloadDirectory);

            await ProcessParallelDownloadsAsync(downloadSource)
                            .ConfigureAwait(false);

            if (!downloadSource.Verbose)
                Console.WriteLine("Download completed.");
        }

        //public async Task DownloadUrlAsync(Uri url)
        //{
        //    using var httpClient = new HttpClient();
        //    var r = await httpClient.GetAsync(url);

        //    var fileBytes = await r.Content.ReadAsByteArrayAsync();
        //    Console.WriteLine(url.AbsoluteUri + "done");

        //}
        public void ExecuteDryRun(bool isVerbose, Download[] downloads)
        {
            if (isVerbose)
            {
                Console.WriteLine("A dry-run shows the followings : ");
                Console.WriteLine();
                foreach (var d in downloads)
                {
                    var s = $"Url : {d.Url}{Environment.NewLine}File: {d.File}{Environment.NewLine}Sha256: {d.Sha256}{Environment.NewLine}Sha1: {d.Sha1}{Environment.NewLine}Overwrite: {d.Overwrite}";
                    Console.WriteLine(s);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("A dry-run is carried out; you're good to download.");
            }
        }

        private async Task ProcessParallelDownloadsAsync(Source downloadSource)
        {
            var taskResults = new List<string>();

            for (var i = 0; i < downloadSource.Downloads.Length; i = i + downloadSource.Config.ParallelDownloads)
            {
                var urlsToBeDownloaded = downloadSource.Downloads
                                                            .Skip(i)
                                                                .Take(downloadSource.Config.ParallelDownloads);

                var tasks = GetDownloadTasks(downloadSource, urlsToBeDownloaded);


                string[] results = await Task.WhenAll(tasks)
                            .ConfigureAwait(false);

                taskResults.AddRange(results);
            }

            var filePaths = taskResults?.Select(r => Path.Combine(downloadSource.Config.DownloadDirectory, r));
            _remoteServerCall.ValidateDownloadedFile(filePaths?.ToArray());
        }

        private IEnumerable<Task<string>> GetDownloadTasks(Source downloadSource, IEnumerable<Download> urlsToBeDownloaded)
        {
            Console.WriteLine();
            foreach (var item in urlsToBeDownloaded)
            {
                Count += 1;

                if (!downloadSource.Verbose)
                    Console.WriteLine($"Download file # {Count}....");

                if (downloadSource.Verbose)
                    Console.WriteLine($"File # {Count} of {downloadSource.Downloads.Length} File name : {item.File.ToUpper()} is downloading");

                var downloadToFilePath = Path.Combine(downloadSource.Config.DownloadDirectory, item.File);
                if (!Path.HasExtension(downloadToFilePath))
                {
                    Console.WriteLine($"File name : {item.File.ToUpper()} does not have an extension");
                    continue;
                }
                var task = _remoteServerCall
                                .DownloadAsync(item.Url, downloadToFilePath, item.Overwrite);

                yield return task;
            }
        }
    }
}
