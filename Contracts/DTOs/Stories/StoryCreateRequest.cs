using System.ComponentModel.DataAnnotations;
using Services.DTOs.Episodes;
using Services.Validators;

namespace Contracts.DTOs.Stories;

public class StoryCreateRequest
{
    [Required]
    [MaxLength(256)]
    public String Name { get; set; }
    [Range(0, 10)]
    public Double Rating { get; set; }
    public String Desription { get; set; }
    public String Thumbnail { get; set; }
    public String SourceDescription { get; set; }
    public String Author { get; set; }
    public String Voice { get; set; }
    [Required]
    public List<long> StoryCategoryId { get; set; }
    [Required]
    public Boolean IsBook { get; set; }
    [Required]
    public Boolean IsStory { get; set; }
    public Int64 CreatedById { get; set; }
    public String CreatedByName { get; set; }
    public DateTime CreatedTime { get; set; }
    [NonEmptyList(ErrorMessage = "Episodes list cannot be empty")]
    public List<EpisodeCreateRequest> Episodes { get; set; }
}