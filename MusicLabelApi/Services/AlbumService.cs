using MusicLabelApi.Models;
using MusicLabelApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MusicLabelApi.Services;

public class AlbumService
{
    MusicDbContext _dbcontext;
    public AlbumService(MusicDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public Album GetAlbumById(int id)
    {
        // var album = _dbcontext.Albums.Find(id);
        // return album;

         var album = _dbcontext.Albums
            .Include(a => a.MusicLabel) // Ensures Musiclabel are loaded
            .Include(a => a.Artists) // Ensures Artist are loaded
            .FirstOrDefault(a => a.Id == id);

        if (album == null)
        {
            throw new KeyNotFoundException($"Album with Id {id} not found.");
        }

        return album;
    }

    public IEnumerable<Album> GetAllAlbums()
    {
        var albums = _dbcontext.Albums.Include(a => a.MusicLabel).Include(a => a.Artists).ToList();
        return albums;
    }

    public Album CreateNewAlbum(Album album)
    {
        _dbcontext.Albums.Add(album);
        _dbcontext.SaveChanges();
        return album;
    }

    public void Update(Album album)
    {
        _dbcontext.Albums.Update(album);
        _dbcontext.SaveChanges();
    }

    public void Delete(int id)
    {
        var album = _dbcontext.Albums.Find(id);
        if (album == null)
        {
            return;
        }
        _dbcontext.Albums.Remove(album);
        _dbcontext.SaveChanges();
    }
}
