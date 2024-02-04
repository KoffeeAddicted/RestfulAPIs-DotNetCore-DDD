namespace Contracts.DTOs.Stories;

public class StoriesFilteredResponse
{
    public Int32 Page { get; set; }
    public Int32 PageSize { get; set; }
    public Int32 TotalCount { get; set; }
    public IEnumerable<StoryResponseDTO> StoryResponseDtos { get; set; }
}