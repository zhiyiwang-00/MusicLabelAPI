namespace MusicLabelApi.Models.DTOs;

public class MusicLabelDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    //public ICollection<Album>? Albums { get; set; }
}
