using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Services;
using MusicLabelApi.Models.DTOs;

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



        [HttpGet("{id}")]
        public ActionResult<ArtistReadDTO> GetArtistById(int id)
        {
            var artist = _artistService.GetArtistById(id);
            // if (artist == null)
            // {
            //     return NotFound();
            // }
            return Ok(artist);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArtistReadDTO>> GetArtists()
        {
            var artists = _artistService.GetAllArtists();
            return Ok(artists);
        }

        // [HttpPost]
        // public ActionResult CreateArtist([FromBody] ArtistCreateDTO artistDto)
        // {
        //     var artist = new Artist
        //     {
        //         FullName = artistDto.FullName,
        //         StageName = artistDto.StageName,
        //         Picture = artistDto.Picture,
        //         Biography = artistDto.Biography
        //     };

        //     _artistService.CreateNewArtist(artist);
        //     return Ok();
        // }

        // [HttpPut("{id}")]
        // public ActionResult UpdateArtist(int id, [FromBody] ArtistUpdateDTO artistDto)
        // {
        //     var existingArtist = _artistService.GetArtistById(id);
        //     if (existingArtist == null)
        //     {
        //         return NotFound();
        //     }

        //     existingArtist.Name = artistDto.Name;
        //     existingArtist.Description = artistDto.Description;

        //     _artistService.Update(existingArtist);
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public ActionResult DeleteArtist(int id)
        // {
        //     if (_artistService.GetArtistById(id) == null)
        //     {
        //         return NotFound();
        //     }
        //     _artistService.Delete(id);
        //     return NoContent();
        // }
    }
        
     
    }
