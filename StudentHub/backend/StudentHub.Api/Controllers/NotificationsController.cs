using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StudentHub.Api.Hubs;
using StudentHub.Api.Models.Notifications;
using StudentHub.Api.Services.Notifications;
using StudentHub.Core.Entities.Notifications;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _service;
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationsController(INotificationService service, IHubContext<NotificationHub> hub)
        {
            _service = service;
            _hub = hub;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUserNotifications()
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null) return Unauthorized();

            var items = await _service.GetForUserAsync(userId);

            return Ok(items.Select(n => new NotificationDto
            {
                Id = n.Id,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult> Create(CreateNotificationDto dto)
        {
            var notification = new Notification
            {
                UserId = dto.UserId,
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            var created = await _service.CreateAsync(notification);

            // Send via SignalR to specific user
            await _hub.Clients.User(dto.UserId).SendAsync("ReceiveNotification", new
            {
                id = created.Id,
                message = created.Message,
                createdAt = created.CreatedAt
            });

            return Ok(created);
        }

        [HttpPatch("{id}/read")]
        public async Task<ActionResult> MarkAsRead(int id)
        {
            var ok = await _service.MarkAsReadAsync(id);
            if (!ok) return NotFound();

            return NoContent();
        }
    }
}
