
namespace DownloadManagerCli.Engine
{
    using DownloadManagerCli.Abstraction.AbstractBase;
    using DownloadManagerCli.Abstraction.AbstractClasses;
    using DownloadManagerCli.Engine.ProcessInputFile;
    using DownloadManagerCli.Model.Enums;
    
    public sealed class ConcreteDownloadSourceFactory : DownloadSourceFactory
    {
        public override DownloadSourceBase GetDownloadSource(string sourceType)
        {
            switch (sourceType)
            {
                case nameof(DownloadSourceEnum.YMAL):
                case nameof(DownloadSourceEnum.YML):
                    return new ReadYamlSource();
                case nameof(DownloadSourceEnum.JSON):
                    return new ReadJsonSource();
                case nameof(DownloadSourceEnum.XML):
                    return new ReadXmlSource();
                default:
                    throw new System.Exception("Source type provided is invalid");
            }
        }
    }
}
