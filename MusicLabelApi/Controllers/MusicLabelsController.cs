using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Models.DTOs;
using MusicLabelApi.Services;

namespace MusicLabelApi.Controllers
{

    [ApiController]
    [Route("api/v1/musiclabels")]

    public class MusicLabelsController : ControllerBase
    {
        private readonly MusicLabelService _musicLabelService;
        private readonly AlbumService _albumService;

        public MusicLabelsController(MusicLabelService musicLabelService, AlbumService albumService)
        {
            _musicLabelService = musicLabelService;
            _albumService = albumService;
        }

        [HttpGet("{id}")]
        public ActionResult<MusicLabelReadDTO> GetMusicLabelById(int id)
        {
            var musicLabel = _musicLabelService.GetMusicLabelById(id);
            // if (musicLabel == null)
            // {
            //     return NotFound();
            // }
            return Ok(musicLabel);
        }

        [HttpGet]
        public ActionResult<IEnumerable<MusicLabelReadDTO>> GetMusicLabels()
        {
            var musicLabels = _musicLabelService.GetAllMusicLabels();
            return Ok(musicLabels);
        }

        [HttpPost]
        public ActionResult CreateMusicLabel([FromBody] MusicLabelCreateDTO musicLabelDto)
        {
            var musicLabel = new MusicLabel
            {
                Name = musicLabelDto.Name,
                Description = musicLabelDto.Description,
                Albums = musicLabelDto.Albums
            };

            _musicLabelService.CreateNewMusicLabel(musicLabel);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMusicLabel(int id, [FromBody] MusicLabelUpdateDTO musicLabelDto)
        {
            var existingMusicLabel = _musicLabelService.GetMusicLabelById(id);
            if (existingMusicLabel == null)
            {
                return NotFound();
            }

            existingMusicLabel.Name = musicLabelDto.Name;
            existingMusicLabel.Description = musicLabelDto.Description;

            _musicLabelService.Update(existingMusicLabel);
            return NoContent();
        }

        [HttpDelete("{id}")]
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

            // musicLabel.Albums = new List<Album>();
            // foreach (var albumId in albumIds)
            // {
            //     var album = _albumService.GetAlbumById(albumId);
            //     if (album != null)
            //     {
            //         musicLabel.Albums.Add(album);
            //     }
            // }
            _musicLabelService.Update(musicLabel);
            return Ok();
        }
    }

}