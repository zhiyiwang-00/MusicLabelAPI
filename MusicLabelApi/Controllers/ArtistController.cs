using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Services;
using MusicLabelApi.Models.DTOs;
using AutoMapper;

namespace MusicLabelApi.Controllers
{
    [ApiController]
    [Route("api/v1/artists")]

    public class ArtistsController : ControllerBase
    {
        private readonly ArtistService _artistService;

        private readonly IMapper _mapper;
        public ArtistsController(ArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<ArtistReadDTO>> GetArtists()
        {
            var artists = _artistService.GetAllArtists();
            var artistsDto = _mapper.Map<IEnumerable<ArtistReadDTO>>(artists);
            return Ok(artistsDto);
        }


        [HttpGet("{id}")]
        public ActionResult<ArtistReadDTO> GetArtistById(int id)
        {
            var artist = _artistService.GetArtistById(id);
             
            var artistDto = _mapper.Map<ArtistReadDTO>(artist);
            // if (artist == null)
            // {
            //     return NotFound();
            // }
            return Ok(artistDto);
        }

        

        [HttpPost]
        public ActionResult CreateArtist([FromBody] ArtistCreateDTO artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto); 

            _artistService.CreateNewArtist(artist);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateArtist(int id, [FromBody] ArtistUpdateDTO artistDto)
        {
            var existingArtist = _artistService.GetArtistById(id);
            if (existingArtist == null)
            {
                return NotFound();
            }

            _mapper.Map(artistDto, existingArtist);

            _artistService.Update(existingArtist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteArtist(int id)
        {
            if (_artistService.GetArtistById(id) == null)
            {
                return NotFound();
            }
            _artistService.Delete(id);
            return NoContent();
        }
    }


}
