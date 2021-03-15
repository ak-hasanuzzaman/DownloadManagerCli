namespace DownloadManagerCli.Abstraction.Interfaces
{
    using System;
    using System.Threading.Tasks;
    public interface IRemoteServerCall
    {
        Task<byte[]> GetBytesAsync(Uri uri);
        Task<byte[]> GetBytesAsync(string ftpPath);
    }
}
