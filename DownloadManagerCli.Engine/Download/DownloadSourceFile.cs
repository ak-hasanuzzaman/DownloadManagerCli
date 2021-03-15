namespace DownloadManagerCli.Engine.Download
{
    using DownloadManagerCli.Abstraction.Interfaces;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    public class RemoteServerCall : IRemoteServerCall
    {
        public async Task<byte[]> GetBytesAsync(Uri uri)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(uri).ConfigureAwait(false); ;

            //var httpStatus = response.EnsureSuccessStatusCode();

            //if (!httpStatus.IsSuccessStatusCode)
            //    Console.WriteLine($"Failed to download from : {uri.AbsoluteUri}");

            var fileBytes = await response.Content.ReadAsByteArrayAsync();
            return fileBytes;
        }
        public Task<byte[]> GetBytesAsync(string uri)
            => throw new NotImplementedException();
    }
}
