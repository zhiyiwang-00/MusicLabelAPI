namespace MusicLabelApi.Models.DTOs;

public class ArtistWithIdDTO
{
    public int id { get; set; }
    public required string FullName { get; set; }
    public string? StageName { get; set; }
    public string? Picture { get; set; }
    public string? Biography { get; set; }
}