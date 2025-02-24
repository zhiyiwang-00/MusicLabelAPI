using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLabelApi.Models;
using MusicLabelApi.Services;
using MusicLabelApi.Models.DTOs;
using MusicLabelApi.Data;
using AutoMapper;

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
        public ActionResult<IEnumerable<AlbumReadDTO>> GetAlbums()
        {
            var albums = _albumService.GetAllAlbums();
            var albumsDto = _mapper.Map<IEnumerable<AlbumReadDTO>>(albums);
            return Ok(albumsDto);
        }


        [HttpGet("{id}")]
        public ActionResult<AlbumReadDTO> GetAlbumById(int id)
        {
            var album = _albumService.GetAlbumById(id);
            var albumDto = _mapper.Map<AlbumReadDTO>(album);
            // if (album == null)
            // {
            //     return NotFound();
            // }
            return Ok(albumDto);
        }

        [HttpPost]
        public ActionResult CreateAlbum([FromBody] AlbumCreateDTO albumDto)
        {
            var album = _mapper.Map<Album>(albumDto); 

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
            _mapper.Map(albumDto, existingAlbum);
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

        [HttpPut("{id}/artists")]
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
