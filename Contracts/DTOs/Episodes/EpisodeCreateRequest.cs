using System.ComponentModel.DataAnnotations;
using Services.DbEnum;

namespace Services.DTOs.Episodes;

public class EpisodeCreateRequest
{
    [Required]
    [Range(1, Int32.MaxValue)]
    public Int64 OrderNumber { get; set; }
    [Required]
    public String File { get; set; }
    [Required]
    public InputUploadAudioTypeEnum InputAudioType { get; set; }
}