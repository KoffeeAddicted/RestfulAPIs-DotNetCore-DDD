using Services.DTOs.Episodes;

namespace Contracts.DTOs.Stories;

public class StoryResponseDTO
{
    public Int64 Id { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public String Thumbnail { get; set; }
    public String SourceDescription { get; set; }
    public IEnumerable<EpisodeResponseDTO> Episodes { get; set; }
}