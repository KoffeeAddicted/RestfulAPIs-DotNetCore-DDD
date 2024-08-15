using Services.DTOs.Episodes;
using Services.DTOs.StoriyCategories;

namespace Contracts.DTOs.Stories;

public class StoryResponseDTO
{
    public Int64 Id { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public String Thumbnail { get; set; }
    public String SourceDescription { get; set; }
    public String Author { get; set; }
    public String Voice { get; set; }
    public IList<StoryCategoryResponseDTO> StoryCategory { get; set; }
    public IEnumerable<EpisodeResponseDTO> Episodes { get; set; }
}

public class StoryResponeAuthorDTO
{
    public String Author { get; set; }
}

public class StoryResponeVoiceDTO
{
    public String Voice { get; set; }
}