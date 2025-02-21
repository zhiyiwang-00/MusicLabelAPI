using MusicLabelApi.Models;
using MusicLabelApi.Data;

namespace MusicLabelApi.Services;

public class ArtistService{
    MusicDbContext _dbcontext;
    public ArtistService(MusicDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public Artist GetArtistById(int id)
    {
        var artist = _dbcontext.Artists.Find(id);
        return artist;
    }

    public IEnumerable<Artist> GetAllArtists()
    {
        var musicLabels = _dbcontext.Artists.ToList();
        return musicLabels;
    }

    public Artist CreateNewArtist(Artist artist)
    {
        _dbcontext.Artists.Add(artist);
        _dbcontext.SaveChanges();
        return artist;
    }

    public void Update(Artist artist)
    {
        _dbcontext.Artists.Update(artist);
        _dbcontext.SaveChanges();
    }

    public void Delete(int id)
    {
        var artist = _dbcontext.Artists.Find(id);
        if (artist == null)
        {
            return;
        }
        _dbcontext.Artists.Remove(artist);
        _dbcontext.SaveChanges();
    }
}
