using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Services;
using MusicLabelApi.Models.DTOs;

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
        
        [HttpGet("{id}")]
        public ActionResult<AlbumReadDTO> GetAlbumById(int id)
        {
            var album = _albumService.GetAlbumById(id);
            // if (album == null)
            // {
            //     return NotFound();
            // }
            return Ok(album);
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumReadDTO>> GetAlbums()
        {
            var albums = _albumService.GetAllAlbums();
            return Ok(albums);
        }

        [HttpPost]
        public ActionResult CreateAlbum([FromBody] AlbumCreateDTO albumDto)
        {
            var album = new Album
            {
                Title = albumDto.Title,
                Genre = albumDto.Genre,
                ReleaseYear = albumDto.ReleaseYear,
                CoverImage = albumDto.CoverImage,
                MusicLabelId = albumDto.MusicLabelId
            };

            _albumService.CreateNewAlbum(album);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAlbum(int id, [FromBody] AlbumUpdateDTO albumDto)
        {
            var existingAlbum = _albumService.GetAlbumById(id);
            if (existingAlbum == null)
            {
                return NotFound();
            }
            existingAlbum.Title = albumDto.Title;
            existingAlbum.Genre = albumDto.Genre;
            existingAlbum.ReleaseYear = albumDto.ReleaseYear;
            existingAlbum.CoverImage = albumDto.CoverImage;
            existingAlbum.MusicLabelId = albumDto.MusicLabelId;
            _albumService.Update(existingAlbum);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAlbum(int id)
        {
            if (_albumService.GetAlbumById(id) == null)
            {
                return NotFound();
            }
            _albumService.Delete(id);
            return Ok();

    }
     
    }
}
