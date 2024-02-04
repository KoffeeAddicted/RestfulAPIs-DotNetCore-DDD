using System.ComponentModel.DataAnnotations;
using Services.DTOs.Episodes;

namespace Contracts.DTOs.Stories;

public class StoryCreateRequest
{
    [Required]
    [MaxLength(256)]
    public String Name { get; set; }
    public Double Rating { get; set; }
    public String Desription { get; set; }
    public String Thumbnail { get; set; }
    public String SourceDescription { get; set; }
    public Int64 CreatedById { get; set; }
    public String CreatedByName { get; set; }
    public DateTime CreatedTime { get; set; }
    public IEnumerable<EpisodeCreateRequest> Episodes { get; set; }
}