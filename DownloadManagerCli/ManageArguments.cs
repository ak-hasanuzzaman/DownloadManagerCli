namespace DownloadManagerCli
{
    using CommandLine;
    using DownloadManagerCli.Abstraction.AbstractBase;
    using DownloadManagerCli.Abstraction.AbstractClasses;
    using DownloadManagerCli.Engine;
    using DownloadManagerCli.Model.CliArgumentOptions;
    using DownloadManagerCli.Model.DownloadSource;
    using DownloadManagerCli.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    internal static class ReadSourceFileUsingArguments
    {
        internal static IEnumerable<Type> LoadVerbs()
            => from assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies()
               let assembly = Assembly.Load(assemblyName)
               from type in assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<VerbAttribute>() != null)
                .ToArray()
               select type;
        internal static void HandleParseError(IEnumerable<Error> errs)
            => Console.WriteLine("Provided arguments can't be parsed successfully");

        public static DownloadSourceBase GetDownloadSource(DownloadOptions options)
        {
            var sourceType = options.FilePath != null ?
                               Path.GetExtension(options.FilePath).Replace(".", "") :
                               null;
            DownloadSourceFactory downloadSourceFactory = new ConcreteDownloadSourceFactory();
            DownloadSourceBase downloadSourceBase = downloadSourceFactory.GetDownloadSource(sourceType.ToUpper());
            return downloadSourceBase;
        }

        internal static void ValidateArguments(DownloadOptions options)
        {
            if (options == null)
                throw new Exception("Cli arguments can't be null");

            if (!File.Exists(options.FilePath))
                throw new Exception("Source file path is invalid");

            var sourceType = options.FilePath != null ?
                                  Path.GetExtension(options.FilePath).Replace(".", "") :
                                  null;
            if (sourceType == null)
                throw new Exception("Source file type can't be empty or null");

            if (!Enum.IsDefined(typeof(DownloadSourceEnum), sourceType?.ToUpper()))
                throw new Exception($"Invalid source file type : {sourceType}");
        }

        internal static void ValidateSourceObject(Source source)
        {
            if (source == null)
                throw new Exception("Source object can't be null");

            if (source.Downloads == null)
                throw new Exception("Download details in the source can't be null");

            if (source.Config == null || source.Config.DownloadDir == null)
                throw new Exception("Target directory can't be null");
        }

        //internal static void Run(object obj)
        //{
        //    switch (obj)
        //    {
        //        case DownloadOptions c:
        //            RunDownloadOptions(c);
        //            break;
        //        case ValidateOptions o:
        //            RunValidateOptions(o);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //private static void RunValidateOptions(ValidateOptions validateOptions)
        //{
        //    if (validateOptions.Verbose)
        //    {
        //        Console.WriteLine(validateOptions.Verbose);
        //    }
        //    Console.ReadLine();
        //}

        //private static void RunDownloadOptions(DownloadOptions opts)
        //{
        //    if (opts.DryRun)
        //    {
        //        Console.WriteLine(opts.DryRun);
        //    }
        //    Console.WriteLine(opts.NumberOfParallelDownloads);
        //}
    }
}
