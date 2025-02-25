using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Services;
using MusicLabelApi.Models.DTOs;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Get all artists",
            Description = "Get all artists from the database"
        )]
        [SwaggerResponse(200, "List of artists", typeof(ArtistWithIdDTO))]
        public ActionResult<IEnumerable<ArtistWithIdDTO>> GetArtists()
        {
            var artists = _artistService.GetAllArtists();
            var artistsDto = _mapper.Map<IEnumerable<ArtistWithIdDTO>>(artists);
            return Ok(artistsDto);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get an artist by id",
            Description = "Get an artist by its unique identifier"
        )]
        [SwaggerResponse(200, "The artist", typeof(ArtistWithIdDTO))]
        [SwaggerResponse(404, "Artist not found")]
        public ActionResult<ArtistWithIdDTO> GetArtistById(int id)
        {
            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }
            var artistDto = _mapper.Map<ArtistWithIdDTO>(artist);

            return Ok(artistDto);
        }



        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new artist",
            Description = "Create a new artist in the database"
        )]
        [SwaggerResponse(200, "The artist was created")]
        public ActionResult CreateArtist([FromBody] ArtistDTO artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);

            _artistService.CreateNewArtist(artist);
            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an artist",
            Description = "Update an artist by its unique identifier"
        )]
        [SwaggerResponse(204, "The artist was updated")]
        [SwaggerResponse(404, "Artist not found")]
        public ActionResult UpdateArtist(int id, [FromBody] ArtistDTO artistDto)
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
        [SwaggerOperation(
            Summary = "Delete an artist",
            Description = "Delete an artist by its unique identifier"
        )]
        [SwaggerResponse(204, "The artist was deleted")]
        [SwaggerResponse(404, "Artist not found")]
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
