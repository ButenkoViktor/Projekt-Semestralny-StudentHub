using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Notes;
using StudentHub.Api.Services.Notes;
using StudentHub.Core.Entities.Notes;
using System.Security.Claims;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _service;

        public NotesController(INoteService service)
        {
            _service = service;
        }

        private string UserId =>
            User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetMyNotes()
        {
            var notes = await _service.GetForUserAsync(UserId);

            return Ok(notes.Select(n => new NoteDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedAt = n.CreatedAt,
                UpdatedAt = n.UpdatedAt
            }));
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> Create(CreateNoteDto dto)
        {
            var note = await _service.CreateAsync(new Note
            {
                UserId = UserId,
                Title = dto.Title,
                Content = dto.Content
            });

            return Ok(new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateNoteDto dto)
        {
            var updated = await _service.UpdateAsync(new Note
            {
                Id = id,
                Title = dto.Title,
                Content = dto.Content
            });

            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id, UserId);
            return ok ? NoContent() : NotFound();
        }
    }
}
