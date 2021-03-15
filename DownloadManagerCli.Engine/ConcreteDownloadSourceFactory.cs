
namespace DownloadManagerCli.Engine
{
    using DownloadManagerCli.Abstraction.AbstractBase;
    using DownloadManagerCli.Abstraction.AbstractClasses;
    using DownloadManagerCli.Engine.DownloadSource;
    using DownloadManagerCli.Model.Enums;
    using System;
    using static DownloadManagerCli.Engine.DownloadSource.ReadYamlSource;

    public sealed class ConcreteDownloadSourceFactory : DownloadSourceFactory
    {
        public override DownloadSourceBase GetDownloadSource(string sourceType)
        {
            switch (sourceType)
            {
                case nameof(DownloadSourceEnum.YAML):
                case nameof(DownloadSourceEnum.YML):
                    return new ReadYamlSource();
                case nameof(DownloadSourceEnum.JSON):
                    return new ReadJsonSource();
                default:
                    throw new System.Exception("Source type provided is invalid");
            }
        }
    }
}
