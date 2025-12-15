using Microsoft.AspNetCore.Mvc;
using DailyDiary;
using System.Collections.Generic;

namespace DailyDiary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiaryController : ControllerBase
    {
        private readonly DiaryManager _manager;

        public DiaryController(DiaryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IEnumerable<DiaryEntry> Get()
        {
            return _manager.GetEntries();
        }

        [HttpPost]
        public IActionResult Post([FromBody] DiaryEntryDTO entryDto)
        {
            if (string.IsNullOrWhiteSpace(entryDto.Content))
            {
                return BadRequest("Content cannot be empty.");
            }
            _manager.AddEntry(entryDto.Content);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_manager.DeleteEntry(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }

    public class DiaryEntryDTO
    {
        public string Content { get; set; }
    }
}
