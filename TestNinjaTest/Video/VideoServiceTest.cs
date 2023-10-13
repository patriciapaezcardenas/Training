using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;
using TestNinja.Videos;

namespace TestNinjaTest
{
    [TestFixture]
    public class VideoServiceTest
    {
        private Mock<IVideoRepository> _repositoryMock;
        private VideoService _videoService;


        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IVideoRepository>();
            _videoService = new VideoService(_repositoryMock.Object);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnsEmptyString()
        {
            _repositoryMock.Setup(rep => rep.GetVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnsString()
        {
            _repositoryMock.Setup(rep => rep.GetVideos()).Returns(new List<Video>()
            {
                new Video { Id = 1},
                new Video { Id = 2 },
                new Video { Id = 3 }
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
