using System.ComponentModel.DataAnnotations;

public class Album{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string Title { get; set; }
    [MaxLength(50)]
    public required string Genre { get; set; }

    public required int ReleaseYear { get; set; }

    public required int MusicLabelId { get; set; }
    public required MusicLabel MusicLabel { get; set; }
    [MaxLength(100)]
    public string? CoverImage { get; set; }

    public ICollection<Artist> Artists { get; set; }

}