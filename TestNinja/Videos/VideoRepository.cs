using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.Videos
{
    public interface IVideoRepository
    {
        List<Video> GetVideos();
    }

    public class VideoRepository : IVideoRepository
    {
        private readonly VideoContext _context;

        public VideoRepository(VideoContext context)
        {
            _context = context;
        }

        public List<Video> GetVideos()
        {
            var videos =
                (from video in _context.Videos
                    where !video.IsProcessed
                    select video).ToList();

            return videos;
        }
    }
}
