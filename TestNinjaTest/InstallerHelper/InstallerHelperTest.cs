using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinjaTest.InstallerHelper
{
    [TestFixture]
    public class InstallerHelperTest
    {
        private Mock<IFileDownloader> fileDownloaderMock;
        private TestNinja.Mocking.InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            fileDownloaderMock = new Mock<IFileDownloader>();
            _installerHelper = new TestNinja.Mocking.InstallerHelper(fileDownloaderMock.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            fileDownloaderMock.Setup(x 
                => x.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();
            
            var result = _installerHelper.DownloadInstaller("customer", null);

            Assert.IsFalse(result);
        }


        [Test]
        public void DownloadInstaller_DownloadCompletes_ReturnTrue()
        {
            var result = _installerHelper.DownloadInstaller("customer", null);

            Assert.IsTrue(result);
        }
    }
}
