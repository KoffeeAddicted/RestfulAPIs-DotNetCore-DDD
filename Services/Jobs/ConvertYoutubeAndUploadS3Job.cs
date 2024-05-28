using Contracts;
using Contracts.Helpers;
using Domain;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Services.DbEnum;

namespace Services.Jobs;

[DisallowConcurrentExecution]
public class ConvertYoutubeAndUploadS3Job : IJob
{
    private readonly ILogger<ConvertYoutubeAndUploadS3Job> _logger;
    private readonly IRepositoryManager _repositoryManager;
    private readonly AppSettings _appSettings;
    
    public ConvertYoutubeAndUploadS3Job(ILogger<ConvertYoutubeAndUploadS3Job> logger,
        IRepositoryManager repositoryManager,
        IOptions<AppSettings> appSettings)
    {
        _logger = logger;
        _repositoryManager = repositoryManager;
        _appSettings = appSettings.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        IEnumerable<Audio> audios = await _repositoryManager.AudioRepository.GetTop10YoutubeVideoAsync();
        
        if(audios.Any())
            await Task.CompletedTask;
        
        foreach (Audio audio in audios)
        {
            String link = await UploadYoutubeVideoToS3(audio.Link);
            audio.Duration = await YoutubeHelper.GetVideoDuration();
            audio.Link = link;
        }

        _repositoryManager.AudioRepository.UpdateAsync(audios.ToList());
        
        await Task.CompletedTask;
    }
    
    private async Task<String> UploadYoutubeVideoToS3(string url)
    {
        YoutubeHelper.url = url;
        String link = string.Empty;
        try
        {
            Stream videoStream = await YoutubeHelper.DownloadYoutubeVideo();
        
            link = await ConvertToAudioHelper.ConvertStreamVideoToAudioStreamAndUploadOnS3(_appSettings, videoStream);
        }
        catch (Exception e)
        {
            return "Error";
        }
        

        return $"{_appSettings.AwsSettings.CloudFrontDomain}/{link}";
    }
}