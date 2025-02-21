namespace MusicLabelApi.Models.DTOs;

public class AlbumReadDTO
{
    public required string Title { get; set; }
    
    public required string Genre { get; set; }

    public required int ReleaseYear { get; set; }

    public int MusicLabelId { get; set; }
    // public MusicLabel? MusicLabel { get; set; }
    
    public string? CoverImage { get; set; }

    public ICollection<Artist>? Artists { get; set; }
}
