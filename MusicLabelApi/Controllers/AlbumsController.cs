using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Services;

namespace MusicLabelApi.Controllers{
    [ApiController]
    [Route("api/v1/albums")]

    public class AlbumsController : ControllerBase
    {
        private readonly AlbumService _albumService;
        public AlbumsController(AlbumService albumService)
        {
            _albumService = albumService;
        }
        
     
    }
}
