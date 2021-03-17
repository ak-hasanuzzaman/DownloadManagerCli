namespace DownloadManagerCli
{
    using System;
    using System.Threading.Tasks;
    internal sealed class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await FileDownloadManager.StartToDownload(args)
                                .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }

    }
}
