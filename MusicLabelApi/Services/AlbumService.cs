using MusicLabelApi.Models;
using MusicLabelApi.Data;

namespace MusicLabelApi.Services;

public class AlbumService{
    MusicDbContext _dbcontext;
    public AlbumService(MusicDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
}
