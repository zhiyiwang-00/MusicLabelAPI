using MusicLabelApi.Models;
using MusicLabelApi.Data;

namespace MusicLabelApi.Services;

public class ArtistService{
    MusicDbContext _dbcontext;
    public ArtistService(MusicDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
}
