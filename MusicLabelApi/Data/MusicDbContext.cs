using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;


namespace MusicLabelApi.Data;
public class MusicDbContext : DbContext
{
    public DbSet<MusicLabel> MusicLabels { get; set; }
    public DbSet<Album> Albums { get; set; }

    public DbSet<Artist> Artists { get; set; }

    public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // MusicLabel to Album (One-to-Many)
        modelBuilder.Entity<MusicLabel>()
            .HasMany(m => m.Albums)
            .WithOne(a => a.MusicLabel)
            .HasForeignKey(a => a.MusicLabelId);

        // Album to Artist (Many-to-Many) with join table configuration
        modelBuilder.Entity<Album>()
            .HasMany(a => a.Artists)
            .WithMany(a => a.Albums)
            .UsingEntity<Dictionary<string, object>>(
                "AlbumArtist",
                r => r.HasOne<Artist>().WithMany().HasForeignKey("ArtistId"),
                l => l.HasOne<Album>().WithMany().HasForeignKey("AlbumId")
            );

    }




    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer("Server=localhost,1433;Database=MusicDB;User Id=sa; Password=dockerStrongPwd123; TrustServerCertificate=True;")
    //     .LogTo(Console.WriteLine, LogLevel.Information);

    // }
}
