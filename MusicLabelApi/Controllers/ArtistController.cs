using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Services;

namespace MusicLabelApi.Controllers{
    [ApiController]
    [Route("api/v1/artists")]

    public class ArtistsController : ControllerBase
    {
        private readonly ArtistService _artistService;
        public ArtistsController(ArtistService artistService)
        {
            _artistService = artistService;
        }
        
     
    }
}
