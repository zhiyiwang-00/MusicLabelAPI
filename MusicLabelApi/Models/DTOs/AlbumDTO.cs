namespace MusicLabelApi.Models.DTOs;

public class AlbumDTO
{
    public required string Title { get; set; }
    
    public required string Genre { get; set; }

    public required int ReleaseYear { get; set; }

    public int MusicLabelId { get; set; }    
    public string? CoverImage { get; set; }
}
