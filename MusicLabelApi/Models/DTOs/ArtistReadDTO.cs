namespace MusicLabelApi.Models.DTOs;

public class ArtistReadDTO
{
    public required string FullName { get; set; }
    public string? StageName { get; set; }
    public string? Picture { get; set; }
    public string? Biography { get; set; }

    // public ICollection<Album>? Albums { get; set; }
}
