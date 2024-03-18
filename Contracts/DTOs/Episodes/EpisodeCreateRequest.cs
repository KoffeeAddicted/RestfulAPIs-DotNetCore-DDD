using System.ComponentModel.DataAnnotations;
using Services.DbEnum;

namespace Services.DTOs.Episodes;

public class EpisodeCreateRequest
{
    [Required]
    [Range(1, Int32.MaxValue)]
    public Int32 OrderNumber { get; set; }
    [FileTypeValidationAttribute]
    public String File { get; set; }
    public Int64 Duration { get; set; }
    [Required]
    public InputUploadAudioTypeEnum InputAudioType { get; set; }
}