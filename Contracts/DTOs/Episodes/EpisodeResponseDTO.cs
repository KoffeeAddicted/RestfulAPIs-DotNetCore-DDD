namespace Services.DTOs.Episodes;

public class EpisodeResponseDTO
{
    public Int64 Id { get; set; }
    public Int32 OrderNumber { get; set; }
    public String Link { get; set; }
    public Int64 Duration { get; set; }
}