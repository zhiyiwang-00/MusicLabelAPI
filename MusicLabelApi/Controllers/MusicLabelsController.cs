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
        public MusicLabelsController(MusicLabelService musicLabelService)
        {
            _musicLabelService = musicLabelService;
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
                Description = musicLabelDto.Description
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
    }

}