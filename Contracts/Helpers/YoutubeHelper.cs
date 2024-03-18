using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace Contracts.Helpers;

public static class YoutubeHelper
{
    public static String url { get; set; } = String.Empty;
    public static async Task<Stream> DownloadYoutubeVideo()
    {
        YoutubeClient youtube = new YoutubeClient();
        Video video = await youtube.Videos.GetAsync(url);

        // Sanitize the video title to remove invalid characters from the file name
        string sanitizedTitle = string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars()));

        // Get all available muxed streams
        StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
        IAudioStreamInfo audioStreamInfo = streamManifest.GetAudioStreams().FirstOrDefault();
        
        using HttpClient httpClient = new HttpClient();
        
        Stream stream = await httpClient.GetStreamAsync(audioStreamInfo.Url);
        return stream;
    }

    public static async Task<String> GetThumbnail(String url)
    {
        YoutubeClient youtube = new YoutubeClient();
        Video video = await youtube.Videos.GetAsync(url);
        
        String thumbnailUrl = video.Thumbnails.FirstOrDefault().Url;
        
        return thumbnailUrl;
    }

    public static async Task<String> GetVideoName()
    {
        YoutubeClient youtube = new YoutubeClient();
        Video video = await youtube.Videos.GetAsync(url);
        
        string sanitizedTitle = string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars()));        
        return sanitizedTitle;
    }
    
    public static async Task<Int64> GetVideoDuration()
    {
        YoutubeClient youtube = new YoutubeClient();
        Video video = await youtube.Videos.GetAsync(url);
        
        if (video.Duration.HasValue)
        {
            // Convert duration to seconds
            Int64 durationInSeconds = (Int64)video.Duration.Value.TotalSeconds;
            return durationInSeconds;
        }

        return 0;
    }
}