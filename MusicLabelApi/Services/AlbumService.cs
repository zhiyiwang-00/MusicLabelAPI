using MusicLabelApi.Models;
using MusicLabelApi.Data;

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
        var album = _dbcontext.Albums.Find(id);
        return album;
    }

    public IEnumerable<Album> GetAllAlbums()
    {
        var albums = _dbcontext.Albums.ToList();
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
