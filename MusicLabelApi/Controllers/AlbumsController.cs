using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Services;
using MusicLabelApi.Models.DTOs;
using MusicLabelApi.Data;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicLabelApi.Controllers
{
    [ApiController]
    [Route("api/v1/albums")]

    public class AlbumsController : ControllerBase
    {
        private readonly AlbumService _albumService;
        private readonly ArtistService _artistService;

        private readonly IMapper _mapper;

        public AlbumsController(AlbumService albumService, ArtistService artistService, IMapper mapper)
        {
            _albumService = albumService;
            _artistService = artistService;
            _mapper = mapper;
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all albums",
            Description = "Get all albums from the database"
        )]
        [SwaggerResponse(200, "List of albums", typeof(AlbumReadDTO))]
        public ActionResult<IEnumerable<AlbumReadDTO>> GetAlbums()
        {
            var albums = _albumService.GetAllAlbums();
            var albumsDto = _mapper.Map<IEnumerable<AlbumReadDTO>>(albums);
            return Ok(albumsDto);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get an album by id",
            Description = "Get an album by its unique identifier"
        )]
        [SwaggerResponse(200, "The album", typeof(AlbumReadDTO))]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult<AlbumReadDTO> GetAlbumById(int id)
        {
            var album = _albumService.GetAlbumById(id);

            if (album == null)
            {
                return NotFound();
            }
            var albumDto = _mapper.Map<AlbumReadDTO>(album);

            return Ok(albumDto);
        }

        [HttpGet("{id}/artists")]
        [SwaggerOperation(
            Summary = "Get all artists of an album",
            Description = "Get all artists of an album by its unique identifier"
        )]
        [SwaggerResponse(200, "List of artists", typeof(ArtistReadDTO))]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult<IEnumerable<ArtistReadDTO>> GetArtistsOfAlbum(int id)
        {
            var album = _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }

            var artistsDto = _mapper.Map<IEnumerable<ArtistReadDTO>>(album.Artists);
            return Ok(artistsDto);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new album",
            Description = "Create a new album in the database"
        )]
        [SwaggerResponse(200, "The album was created")]
        public ActionResult CreateAlbum([FromBody] AlbumCreateDTO albumDto)
        {
            var album = _mapper.Map<Album>(albumDto);

            _albumService.CreateNewAlbum(album);
            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an album",
            Description = "Update an album by its unique identifier"
        )]
        [SwaggerResponse(204, "The album was updated")]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult UpdateAlbum(int id, [FromBody] AlbumUpdateDTO albumDto)
        {
            var existingAlbum = _albumService.GetAlbumById(id);
            if (existingAlbum == null)
            {
                return NotFound();
            }
            _mapper.Map(albumDto, existingAlbum);
            _albumService.Update(existingAlbum);
            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete an album",
            Description = "Delete an album by its unique identifier"
        )]
        [SwaggerResponse(200, "The album was deleted")]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult DeleteAlbum(int id)
        {
            if (_albumService.GetAlbumById(id) == null)
            {
                return NotFound();
            }
            _albumService.Delete(id);
            return Ok();

        }

        [HttpPut("{id}/artists")]
        [SwaggerOperation(
            Summary = "Update artists of an album",
            Description = "Update artists of an album by its unique identifier"
        )]
        [SwaggerResponse(200, "Artists of the album updated")]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult UpdateArtistsOfAlbum(int id, [FromBody] List<int> artistIds)
        {
            var album = _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            album.Artists = new List<Artist>();
            foreach (var artistId in artistIds)
            {
                var artist = _artistService.GetArtistById(artistId);
                if (artist != null)
                {
                    album.Artists.Add(artist);
                }
            }
            _albumService.Update(album);
            return Ok();
        }
    }

}
