namespace Domain.RepositoryInterfaces;

public interface IAudioRepository
{
    Task<IEnumerable<Audio>> GetTop10YoutubeVideoAsync();
    void UpdateAsync(List<Audio> audios);
}