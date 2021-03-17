namespace DownloadManagerCli.Engine.ProcessInputFile
{
    using DownloadManagerCli.Abstraction.AbstractBase;
    using DownloadManagerCli.Model.DownloadSource;
    using System;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;
    public sealed partial class ReadXmlSource : DownloadSourceBase
    {
        public sealed override Source GetSourceObjectFromFile(string path)
        {
            return base.GetSourceObjectFromFile(path);
        }
        protected sealed override Source ConverSourceToObject(string yaml)
        {
            throw new NotImplementedException();
        }
    }
}
