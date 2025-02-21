using MusicLabelApi.Models;
using MusicLabelApi.Data;
using MusicLabelApi.Models.DTOs;

namespace MusicLabelApi.Services;


public class MusicLabelService
{
    MusicDbContext _dbcontext;
    
    public MusicLabelService(MusicDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public MusicLabel GetMusicLabelById(int id)
    {
        var musicLabel = _dbcontext.MusicLabels.Find(id);
        return musicLabel;
    }

    public IEnumerable<MusicLabel> GetAllMusicLabels()
    {
        var musicLabels = _dbcontext.MusicLabels.ToList();
        return musicLabels;
    }

    public MusicLabel CreateNewMusicLabel(MusicLabel musicLabel)
    {
        _dbcontext.MusicLabels.Add(musicLabel);
        _dbcontext.SaveChanges();
        return musicLabel;
    }

    public void Update(MusicLabel musicLabel)
    {
        _dbcontext.MusicLabels.Update(musicLabel);
        _dbcontext.SaveChanges();
    }

    public void Delete(int id)
    {
        var musicLabel = _dbcontext.MusicLabels.Find(id);
        if (musicLabel == null)
        {
            return;
        }
        _dbcontext.MusicLabels.Remove(musicLabel);
        _dbcontext.SaveChanges();
    }

}