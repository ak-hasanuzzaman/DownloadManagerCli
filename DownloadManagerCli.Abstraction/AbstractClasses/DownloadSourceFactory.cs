using DownloadManagerCli.Abstraction.AbstractBase;

namespace DownloadManagerCli.Abstraction.AbstractClasses
{
    public abstract class DownloadSourceFactory
    {
        public abstract DownloadSourceBase GetDownloadSource(string sourceType);
    }
}
