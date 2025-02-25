using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Models.DTOs;
using MusicLabelApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicLabelApi.Controllers
{

    [ApiController]
    [Route("api/v1/musiclabels")]

    public class MusicLabelsController : ControllerBase
    {
        private readonly MusicLabelService _musicLabelService;
        private readonly AlbumService _albumService;

        private readonly IMapper _mapper;

        public MusicLabelsController(MusicLabelService musicLabelService, AlbumService albumService, IMapper mapper)
        {
            _musicLabelService = musicLabelService;
            _albumService = albumService;
            _mapper = mapper;
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all music labels",
            Description = "Get all music labels from the database"
        )]
        [SwaggerResponse(200, "List of music labels", typeof(MusicLabelWithIdDTO))]
        public ActionResult<IEnumerable<MusicLabelWithIdDTO>> GetMusicLabels()
        {
            var musicLabels = _musicLabelService.GetAllMusicLabels();
            var musicLabelsDto = _mapper.Map<IEnumerable<MusicLabelWithIdDTO>>(musicLabels);
            return Ok(musicLabelsDto);

        }

         [HttpGet("{id}")]
         [SwaggerOperation(
             Summary = "Get a music label by id",
             Description = "Get a music label by its unique identifier"
         )]
         [SwaggerResponse(200, "The music label", typeof(MusicLabelWithIdDTO))]
         [SwaggerResponse(404, "Music label not found")]
        public ActionResult<MusicLabelWithIdDTO> GetMusicLabelById(int id)
        {
            var musicLabel = _musicLabelService.GetMusicLabelById(id);
             if (musicLabel == null)
            {
                return NotFound();
            }
            var musicLabelsDto = _mapper.Map<MusicLabelWithIdDTO>(musicLabel);
            return Ok(musicLabelsDto);
        }

        [HttpGet ("{id}/albums")]
        [SwaggerOperation(
            Summary = "Get all albums of a music label",
            Description = "Get all albums of a music label by its unique identifier"
        )]
        [SwaggerResponse(200, "List of albums", typeof(AlbumWithIdDTO))]
        [SwaggerResponse(404, "Music label not found")]
        public ActionResult<IEnumerable<AlbumWithIdDTO>> GetAlbumsOfMusicLabel(int id)
        {
            var musicLabel = _musicLabelService.GetMusicLabelById(id);
            if (musicLabel == null)
            {
                return NotFound();
            }

            var albumsDto = _mapper.Map<IEnumerable<AlbumWithIdDTO>>(musicLabel.Albums);
            return Ok(albumsDto);
        }

        [HttpGet ("{id}/artists")]
        [SwaggerOperation(
            Summary = "Get all artists of a music label",
            Description = "Get all artists of a music label by its unique identifier"
        )]
        [SwaggerResponse(200, "List of artists", typeof(ArtistWithIdDTO))]
        [SwaggerResponse(404, "Music label not found")]
        public ActionResult<IEnumerable<ArtistWithIdDTO>> GetArtistsOfMusicLabel(int id)
        {
            var musicLabel = _musicLabelService.GetMusicLabelById(id);
            if (musicLabel == null)
            {
                return NotFound();
            }

            var artists = new List<Artist>();
            if (musicLabel.Albums != null)
            {
                artists = musicLabel.Albums.SelectMany(a => a.Artists).Distinct().ToList();
            }
            var artistDto = _mapper.Map<IEnumerable<ArtistWithIdDTO>>(artists);
            return Ok(artistDto);
            
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new music label",
            Description = "Create a new music label in the database"
        )]
        [SwaggerResponse(200, "Music label created")]
        public ActionResult CreateMusicLabel([FromBody] MusicLabelDTO musicLabelDto)
        {
            var musicLabel = _mapper.Map<MusicLabel>(musicLabelDto); 

            _musicLabelService.CreateNewMusicLabel(musicLabel);
            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update a music label",
            Description = "Update a music label in the database"
        )]
        [SwaggerResponse(204, "Music label updated")]
        [SwaggerResponse(404, "Music label not found")]
        public ActionResult UpdateMusicLabel(int id, [FromBody] MusicLabelDTO musicLabelDto)
        {
            var existingMusicLabel = _musicLabelService.GetMusicLabelById(id);
            if (existingMusicLabel == null)
            {
                return NotFound();
            }

            _mapper.Map(musicLabelDto, existingMusicLabel);

            _musicLabelService.Update(existingMusicLabel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a music label",
            Description = "Delete a music label from the database"
        )]
        [SwaggerResponse(204, "Music label deleted")]
        [SwaggerResponse(404, "Music label not found")]
        public ActionResult DeleteMusicLabel(int id)
        {
            if (_musicLabelService.GetMusicLabelById(id) == null)
            {
                return NotFound();
            }
            _musicLabelService.Delete(id);
            return NoContent();
        }


        [HttpPut("{id}/albums")]
        [SwaggerOperation(
            Summary = "Update albums of a music label",
            Description = "Update albums of a music label in the database"
        )]
        [SwaggerResponse(200, "Albums updated")]
        [SwaggerResponse(400, "Some albums were not found")]
        [SwaggerResponse(404, "Music label not found")]
        public ActionResult UpdateAlbumsOfMusicLabel(int id, [FromBody] List<int> albumIds)
        {
            var musicLabel = _musicLabelService.GetMusicLabelById(id);
            if (musicLabel == null)
            {
                return NotFound();
            }

            var albums = new List<Album>();
            foreach (var albumId in albumIds)
            {
                var album = _albumService.GetAlbumById(albumId);
                if (album != null)
                {
                    albums.Add(album);
                }
            }
            if (albums.Count != albumIds.Count)
            {
                return BadRequest("Some albums were not found");
            }

            foreach (var album in albums)
            {
                album.MusicLabelId = musicLabel.Id;
                _albumService.Update(album);
            }
            _musicLabelService.Update(musicLabel);
            return Ok();
        }
    }

}