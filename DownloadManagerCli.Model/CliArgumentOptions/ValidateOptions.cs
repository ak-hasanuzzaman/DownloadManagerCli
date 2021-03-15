namespace DownloadManagerCli.Model.CliArgumentOptions
{
    using CommandLine;
    using CommandLine.Text;
    using System.Collections.Generic;

    [Verb("validate", HelpText = "Validate a YAML file.")]
    public class ValidateOptions
    {
        [Option(
         'v',
         "verbose",
         Required = false,
         HelpText = DownloadOptionsConstant.Validate_Verbose)]
        public bool Verbose { get; set; }

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
