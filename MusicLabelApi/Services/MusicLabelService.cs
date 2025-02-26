using MusicLabelApi.Models;
using MusicLabelApi.Data;
using MusicLabelApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MusicLabelApi.Services;


public class MusicLabelService
{
    MusicDbContext _dbcontext;
    
    public MusicLabelService(MusicDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }


    public IEnumerable<MusicLabel> GetAllMusicLabels()
    {
        var musicLabels = _dbcontext.MusicLabels.ToList();
        return musicLabels;
    }

    
    public MusicLabel GetMusicLabelById(int id)
    {
        var musicLabel = _dbcontext.MusicLabels
            .Include(ml => ml.Albums) 
            .ThenInclude(a => a.Artists) 
            .FirstOrDefault(ml => ml.Id == id);

        if (musicLabel == null)
        {
            throw new KeyNotFoundException($"MusicLabel with Id {id} not found.");
        }
        
        return musicLabel;
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