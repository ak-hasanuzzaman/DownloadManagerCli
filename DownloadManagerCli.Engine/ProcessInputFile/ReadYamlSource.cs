namespace DownloadManagerCli.Engine.ProcessInputFile
{
    using DownloadManagerCli.Abstraction.AbstractBase;
    using DownloadManagerCli.Model.DownloadSource;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;
    public sealed partial class ReadYamlSource : DownloadSourceBase
    {
        public sealed override Source GetSourceObjectFromFile(string path)
        {
            return base.GetSourceObjectFromFile(path);
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
