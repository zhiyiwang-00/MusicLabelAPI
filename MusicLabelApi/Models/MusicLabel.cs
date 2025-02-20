using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace MusicLabelApi.Models;
public class MusicLabel
{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }

    public ICollection<Album>? Albums { get; set; }
}

