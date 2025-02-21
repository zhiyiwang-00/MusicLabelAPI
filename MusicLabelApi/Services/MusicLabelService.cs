using MusicLabelApi.Models;
using MusicLabelApi.Data;

namespace MusicLabelApi.Services;

public class MusicLabelService{
    MusicDbContext _dbcontext;
    public MusicLabelService(MusicDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
}
