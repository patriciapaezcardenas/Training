using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using Newtonsoft.Json;

namespace TestNinja.Videos
{
    public class VideoService
    {
        private IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }
        public string ReadVideoTitle()
        {
            var str = File.ReadAllText("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _videoRepository.GetVideos();

                foreach (var v in videos)
                    videoIds.Add(v.Id);

                return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}