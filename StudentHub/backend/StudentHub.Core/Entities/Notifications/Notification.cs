using System;
using StudentHub.Core.Entities.Identity;
namespace StudentHub.Core.Entities.Notifications
{
    public class Notification
    {
        public int Id { get; set; }

        public string UserId { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;

        public string Message { get; set; } = default!;

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}