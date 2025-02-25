namespace MusicLabelApi.Models.DTOs;

public class MusicLabelWithIdDTO
{
    public int id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    //public ICollection<Album>? Albums { get; set; }
}
