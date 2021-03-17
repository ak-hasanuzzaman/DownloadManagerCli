namespace DownloadManagerCli.Model.DownloadSource
{
    public class Source
    {
        public Config Config { get; set; }
        public Download[] Downloads { get; set; }
        public bool Verbose { get; set; }
    }
}
