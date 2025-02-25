using System.ComponentModel.DataAnnotations;

namespace MusicLabelApi.Models;
public class Album{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string Title { get; set; }
    [MaxLength(50)]
    public required string Genre { get; set; }

    public required int ReleaseYear { get; set; }

    public int MusicLabelId { get; set; }
    public MusicLabel? MusicLabel { get; set; }
    [MaxLength(100)]
    public string? CoverImage { get; set; }

    public bool IsDeleted { get; set; } = false;

    public ICollection<Artist>? Artists { get; set; }

}