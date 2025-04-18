namespace MusicLabelApi.Models.DTOs;

public class AlbumWithArtistsReadDTO
{
    public int Id { get; set; }

    public required string Title { get; set; }
    
    public required string Genre { get; set; }

    public required int ReleaseYear { get; set; }

    public int MusicLabelId { get; set; }
    
    public string? CoverImage { get; set; }

    public ICollection<ArtistSimpleReadDTO>? Artists { get; set; }
}
