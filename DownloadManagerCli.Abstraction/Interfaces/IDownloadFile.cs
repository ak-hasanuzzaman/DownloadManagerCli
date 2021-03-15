using DownloadManagerCli.Model.DownloadSource;
using System.Threading.Tasks;

namespace DownloadManagerCli.Abstraction.Interfaces
{
    public interface IDownloadSourceFile
    {
        Task DownloadAsync(Source downloadSource);
        void ExecuteDryRun(Download[] downloads);
    }
}
