using DownloadManagerCli.Abstraction.AbstractBase;
using DownloadManagerCli.Abstraction.AbstractClasses;
using DownloadManagerCli.Abstraction.Interfaces;
using DownloadManagerCli.Model.DownloadSource;
using DownloadManagerCli.Model.Enums;
using System;
using System.IO;

namespace DownloadManagerCli.Engine.ValidateInputSource
{
    public class ValidateSource : IValidateSource
    {
        public void Validate(string sourceFile)
        {
            ValidateArguments(sourceFile);

            var fileExtension = Path.GetExtension(sourceFile);
            DownloadSourceBase downloadSourceBase = GetDownloadSource(fileExtension);
            var source = downloadSourceBase .GetSourceObjectFromFile(sourceFile);

            ValidateSourceContent(source);
        }
        private static DownloadSourceBase GetDownloadSource(string fileExtension)
        {
            DownloadSourceFactory downloadSourceFactory = new ConcreteDownloadSourceFactory();
            DownloadSourceBase downloadSourceBase = downloadSourceFactory.GetDownloadSource(fileExtension.ToUpper());
            return downloadSourceBase;
        }

        private  void ValidateArguments(string sourcePath)
        {
            if (sourcePath == null)
                throw new Exception("Cli arguments can't be null");

            if (!File.Exists(sourcePath))
                throw new Exception("Source file path is invalid");

            var sourceType = sourcePath != null ?
                                  Path.GetExtension(sourcePath).Replace(".", "") :
                                  null;
            if (sourceType == null)
                throw new Exception("Source file type can't be empty or null");

            if (!Enum.IsDefined(typeof(DownloadSourceEnum), sourceType?.ToUpper()))
                throw new Exception($"Invalid source file type : {sourceType}");
        }

        private  void ValidateSourceContent(Source source)
        {
            if (source == null)
                throw new Exception("Source object can't be null");

            if (source.Downloads == null)
                throw new Exception("Download details in the source can't be null");

            if (source.Config == null || source.Config.DownloadDirectory == null)
                throw new Exception("Target directory can't be null");
        }
    }
}
