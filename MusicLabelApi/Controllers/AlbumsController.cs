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


        [HttpGet("simple")]
        [SwaggerOperation(
            Summary = "Get all albums",
            Description = "Get all albums from the database"
        )]
        [SwaggerResponse(200, "List of albums", typeof(AlbumWithIdDTO))]
        public ActionResult<IEnumerable<AlbumWithIdDTO>> GetAlbums()
        {
            var albums = _albumService.GetAllAlbums();
            var albumsDto = _mapper.Map<IEnumerable<AlbumWithIdDTO>>(albums);
            return Ok(albumsDto);
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all albums with optional inclusion of soft-deleted records",
            Description = "Get all albums from the database including soft deleted ones if the includeDeleted flag is set to true"
        )]
        [SwaggerResponse(200, "List of albums", typeof(AlbumWithIdDTO))]
        public ActionResult<IEnumerable<AlbumWithIdDTO>> GetArtistsWithDeleteFlag([FromQuery] bool includeDeleted = false)
        {
            var albums = _albumService.GetAllAlbums(includeDeleted);
            var albumsDto = _mapper.Map<IEnumerable<AlbumWithIdDTO>>(albums);
            return Ok(albumsDto);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get an album by id",
            Description = "Get an album by its unique identifier"
        )]
        [SwaggerResponse(200, "The album", typeof(AlbumWithIdDTO))]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult<AlbumWithIdDTO> GetAlbumById(int id)
        {
            var album = _albumService.GetAlbumById(id);

            if (album == null)
            {
                return NotFound();
            }
            var albumDto = _mapper.Map<AlbumWithIdDTO>(album);

            return Ok(albumDto);
        }


        [HttpGet("{id}/artists")]
        [SwaggerOperation(
            Summary = "Get all artists of an album",
            Description = "Get all artists of an album by its unique identifier"
        )]
        [SwaggerResponse(200, "List of artists", typeof(ArtistWithIdDTO))]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult<IEnumerable<ArtistWithIdDTO>> GetArtistsOfAlbum(int id)
        {
            var album = _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }

            var artistsDto = _mapper.Map<IEnumerable<ArtistWithIdDTO>>(album.Artists);
            return Ok(artistsDto);
        }


        [HttpGet("search")]
        [SwaggerOperation(
            Summary = "Search albums by genre, release year, or label name",
            Description = "Search albums by genre, release year and label name"
        )]
        [SwaggerResponse(200, "List of albums", typeof(AlbumWithIdDTO))]
        public ActionResult<IEnumerable<AlbumWithIdDTO>> SearchAlbums(
            [FromQuery] string? genre,
            [FromQuery] int? releaseYear,
            [FromQuery] string? labelName)
        {
            var query = _albumService.GetAllAlbums().AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                genre = genre.ToLower();
                query = query.Where(a => a.Genre.ToLower() == genre);
            }

            if (releaseYear.HasValue)
            {
                query = query.Where(a => a.ReleaseYear == releaseYear);
            }

            if (!string.IsNullOrEmpty(labelName))
            {
                labelName = labelName.ToLower();
                query = query.Where(a => a.MusicLabel.Name.ToLower() == labelName);
            }

            var albums = query.ToList();
            var albumsDto = _mapper.Map<IEnumerable<AlbumWithIdDTO>>(albums);
            return Ok(albumsDto);
        }


        [HttpGet("{id}/conditional-artists")]
        [SwaggerOperation(
            Summary = "Get an album by id, with conditional artists",
            Description = "Get an album by its unique identifier, with conditional artists based on query parameter or header"
        )]
        [SwaggerResponse(200, "The album", typeof(AlbumWithIdDTO))]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult<AlbumWithArtistsReadDTO> GetAlbumWithConditionalArtists(int id, [FromQuery] bool includeArtists = false, [FromHeader(Name = "X-Include-Artist")] string includeArtistsHeader = null)
        {
            var includeArtistsFlag = includeArtists || includeArtistsHeader == "true";
            var albumQuery = _albumService.GetAlbumByIdWithArtist(id, includeArtistsFlag);

            if (albumQuery == null)
            {
                return NotFound();
            }
            if (includeArtistsFlag)
            {
                var albumDto = _mapper.Map<AlbumWithArtistsReadDTO>(albumQuery);
                albumDto.Artists = _mapper.Map<ICollection<ArtistSimpleReadDTO>>(albumQuery.Artists);
                return Ok(albumDto);
            }
            else
            {
                var albumDto = _mapper.Map<AlbumWithIdDTO>(albumQuery);
                return Ok(albumDto);
            }
        }


        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new album",
            Description = "Create a new album in the database"
        )]
        [SwaggerResponse(200, "The album was created")]
        public ActionResult CreateAlbum([FromBody] AlbumDTO albumDto)
        {
            var album = _mapper.Map<Album>(albumDto);

            _albumService.CreateNewAlbum(album);
            return Ok();
        }


        [HttpPost("bulk")]
        [SwaggerOperation(
            Summary = "Create multiple albums",
            Description = "Create multiple albums in the database"
        )]
        [SwaggerResponse(200, "The albums were created")]
        public ActionResult CreateBulkAlbums([FromBody] List<AlbumDTO> albumDtos)
        {
            var albums = _mapper.Map<IEnumerable<Album>>(albumDtos);
            foreach (var album in albums)
            {
                _albumService.CreateNewAlbum(album);
            }
            return Ok();
        }


        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an album",
            Description = "Update an album by its unique identifier"
        )]
        [SwaggerResponse(204, "The album was updated")]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult UpdateAlbum(int id, [FromBody] AlbumDTO albumDto)
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


        [HttpDelete("{id}/soft_delete")]
        [SwaggerOperation(
            Summary = "Delete an album (soft delete)",
            Description = "Delete an album by its unique identifier (soft delete)"
        )]
        [SwaggerResponse(204, "The album was soft deleted")]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult SoftDeleteAlbum(int id)
        {
            var album = _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            album.IsDeleted = true;
            _albumService.Update(album);
            return NoContent();
        }


        [HttpPost("{id}/restore")]
        [SwaggerOperation(
            Summary = "Restore an soft deleted album ",
            Description = "Restore an album by its unique identifier, if it was soft deleted"
        )]
        [SwaggerResponse(204, "The album was restored")]
        [SwaggerResponse(404, "Album not found")]
        public ActionResult RestoreAlbum(int id)
        {
            var album = _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            album.IsDeleted = false;
            _albumService.Update(album);
            return NoContent();
        }
    }
}
