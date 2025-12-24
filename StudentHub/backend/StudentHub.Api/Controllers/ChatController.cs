using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Chat;
using StudentHub.Api.Services.Chat;
using System.Security.Claims;

[ApiController]
[Route("api/chat")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IChatService _service;

    public ChatController(IChatService service)
    {
        _service = service;
    }

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet("rooms")]
    public async Task<IActionResult> Rooms()
        => Ok(await _service.GetRoomsAsync(UserId));

    [HttpGet("room/{id}")]
    public async Task<IActionResult> Room(int id)
    {
        var room = await _service.GetRoomAsync(id, UserId);
        return room == null ? NotFound() : Ok(room);
    }

    [HttpPost("room/{targetUserId}")]
    public async Task<IActionResult> CreateRoom(string targetUserId)
        => Ok(await _service.CreateOrGetRoomAsync(UserId, targetUserId));

    [HttpPost("send")]
    public async Task<IActionResult> Send(SendMessageRequest req)
        => Ok(await _service.SendMessageAsync(UserId, req));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
        => Ok(await _service.SearchUsersAsync(q, UserId));
}
