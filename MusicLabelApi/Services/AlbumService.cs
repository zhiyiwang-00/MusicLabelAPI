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


    public IEnumerable<Album> GetAllAlbums()
    {
        var albums = _dbcontext.Albums.Include(a => a.MusicLabel).Include(a => a.Artists).ToList();
        return albums;
    }

    public IEnumerable<Album> GetAllAlbums(bool includeDeleted)
    {
        var query = _dbcontext.Albums.AsQueryable();
        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }
        return query.Include(a => a.MusicLabel).Include(a => a.Artists).ToList();
    }


    public Album GetAlbumById(int id)
    {
        var album = _dbcontext.Albums
           .Include(a => a.MusicLabel)
           .Include(a => a.Artists)
           .FirstOrDefault(a => a.Id == id);

        if (album == null)
        {
            throw new KeyNotFoundException($"Album with Id {id} not found.");
        }

        return album;
    }


    public Album GetAlbumByIdWithArtist(int id, bool includeArtists = false)
    {
        var query = _dbcontext.Albums.AsQueryable();
        if (includeArtists)
        {
            query = query.Include(a => a.Artists);
        }
        return query.FirstOrDefault(a => a.Id == id);
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
