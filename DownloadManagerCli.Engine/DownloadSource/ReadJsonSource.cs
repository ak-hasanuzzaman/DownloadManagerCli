namespace DownloadManagerCli.Engine.DownloadSource
{
    using DownloadManagerCli.Abstraction.AbstractBase;
    using DownloadManagerCli.Model.DownloadSource;
    using System;
    public sealed partial class ReadYamlSource
    {
        public sealed class ReadJsonSource : DownloadSourceBase
        {
            public sealed override Source GetDownloadSourceFromFile(string path)
            {
                return base.GetDownloadSourceFromFile(path);
            }
            protected sealed override Source ConverSourceToObject(string json)
            {
                throw new NotImplementedException();
            }
        }
    }
}
