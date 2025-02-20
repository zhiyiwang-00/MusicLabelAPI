using System.ComponentModel.DataAnnotations;

public class Artist{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string FullName { get; set; }
    [MaxLength(50)]
    public string? StageName { get; set; }
    [MaxLength(100)]
    public string? Picture { get; set; }
    [MaxLength(100)]
    public string? Biography { get; set; }

    public ICollection<Album> Albums { get; set; }

}