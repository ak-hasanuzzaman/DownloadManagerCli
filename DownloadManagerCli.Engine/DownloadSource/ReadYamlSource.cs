namespace DownloadManagerCli.Engine.DownloadSource
{
    using DownloadManagerCli.Abstraction.AbstractBase;
    using DownloadManagerCli.Model.DownloadSource;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;
    public sealed partial class ReadYamlSource : DownloadSourceBase
    {
        public sealed override Source GetDownloadSourceFromFile(string path)
        {
            return base.GetDownloadSourceFromFile(path);
        }
        protected sealed override Source ConverSourceToObject(string yaml)
        {
            var deserializer = new DeserializerBuilder()
                   .WithNamingConvention(UnderscoredNamingConvention.Instance)
                   .Build();

            var source = deserializer.Deserialize<Source>(yaml);
            return source;
        }
    }
}
