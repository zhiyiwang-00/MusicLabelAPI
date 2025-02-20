using Microsoft.EntityFrameworkCore;



public class MusicDbContext : DbContext
{
    public DbSet<MusicLabel> MusicLabels { get; set; }
    public DbSet<Album> Albums { get; set; }

    public DbSet<Artist> Artists { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=MusicDB;User Id=sa; Password=dockerStrongPwd123; TrustServerCertificate=True;")
        .LogTo(Console.WriteLine, LogLevel.Information);

    }
}
