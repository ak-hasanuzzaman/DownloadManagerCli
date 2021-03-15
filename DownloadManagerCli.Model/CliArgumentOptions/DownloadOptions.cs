﻿namespace DownloadManagerCli.Model.CliArgumentOptions
{
    using CommandLine;
    using CommandLine.Text;
    using System.Collections.Generic;

    [Verb("download", HelpText = "Download files into a directory.")]
    public class DownloadOptions
    {
        [Option(
         'v',
         "verbose",
         Required = false,
         HelpText = DownloadOptionsConstant.Download_Verbose)]
        public bool Verbose { get; set; }

        [Option(
            'r',
            "dryrun",
            Required = false,
            HelpText = DownloadOptionsConstant.Download_DryRun)]
        public bool DryRun { get; set; }

        [Option(
        'f',
        "filepath",
        Required = true,
        HelpText = DownloadOptionsConstant.Download_FilePath)]
        public string FilePath { get; set; }

        [Option(
            'd',
            "download",
            Required = false,
            HelpText = DownloadOptionsConstant.Download_DryRun)]
        public bool Download { get; set; }

        [Option(
            'p',
            "paralleldownloadsnumber",
            Required = false,
            HelpText = DownloadOptionsConstant.Download_ParallelDownloads)]
        public int NumberOfParallelDownloads { get; set; }

        [Usage(ApplicationAlias = "Download manager cli")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Normal scenario", new DownloadOptions { DryRun = true });
            }
        }

    }

}
