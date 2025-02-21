using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Services;

namespace MusicLabelApi.Controllers{
    [ApiController]
    [Route("api/v1/musiclabels")]

    public class MusicLabelsController : ControllerBase
    {
        private readonly MusicLabelService _musicLabelService;
        public MusicLabelsController(MusicLabelService musicLabelService)
        {
            _musicLabelService = musicLabelService;
        }
        
     
    }
}
