namespace DownloadManagerCli.Abstraction.AbstractBase
{
    using DownloadManagerCli.Model.DownloadSource;
    using System.IO;
    public abstract class DownloadSourceBase
    {
        protected abstract Source ConverSourceToObject(string fileText);
        public virtual Source GetSourceObjectFromFile(string path)
        {
            var source = File.ReadAllText(path);
            var downloadSource = ConverSourceToObject(source);
            return downloadSource;
        }
    }
}
