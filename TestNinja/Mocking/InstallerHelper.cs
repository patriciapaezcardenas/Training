using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private readonly IFileDownloader _downloader;

        private string _setupDestinationFile;
        
        public InstallerHelper(IFileDownloader downloader)
        {
            _downloader = downloader;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                string path = string.Format("http://example.com/{0}/{1}",
                    customerName,
                    installerName);

                _downloader.DownloadFile(path, _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }

    public interface IFileDownloader
    {
        void DownloadFile(string url, string path);
    }

    public class FileDownloader : IFileDownloader
    {
        private WebClient _client;

        public FileDownloader()
        {
            _client = new WebClient();
        }

        public void DownloadFile(string url, string path)
        {
            _client.DownloadFile(url,
                path);
        }
    }
}