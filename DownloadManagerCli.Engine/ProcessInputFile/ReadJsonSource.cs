namespace DownloadManagerCli.Engine.ProcessInputFile
{
    using DownloadManagerCli.Abstraction.AbstractBase;
    using DownloadManagerCli.Model.DownloadSource;
    using System;
    public sealed class ReadJsonSource : DownloadSourceBase
    {
        public sealed override Source GetSourceObjectFromFile(string path)
        {
            return base.GetSourceObjectFromFile(path);
        }
        protected sealed override Source ConverSourceToObject(string json)
        {
            throw new NotImplementedException();
        }
    }
}
