using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Chat;
using StudentHub.Api.Services.Chat;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _service;

        public ChatController(IChatService service)
        {
            _service = service;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [Authorize]
        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms()
        {
            return Ok(await _service.GetRoomsAsync(GetUserId()));
        }

        [Authorize]
        [HttpGet("room/{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _service.GetRoomAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [Authorize]
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            var userId = GetUserId();
            var msg = await _service.SendMessageAsync(userId, request);
            return Ok(msg);
        }
    }
}