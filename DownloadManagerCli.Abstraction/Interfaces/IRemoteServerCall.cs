namespace DownloadManagerCli.Abstraction.Interfaces
{
    using DownloadManagerCli.Model.DownloadSource;
    using System;
    using System.Threading.Tasks;
    public interface IRemoteServerCall
    {
        Task<string> DownloadAsync(Uri uri, string downloadToFilePath, bool overwrite);
        void ValidateDownloadedFile(string[] filePaths);
    }
}
