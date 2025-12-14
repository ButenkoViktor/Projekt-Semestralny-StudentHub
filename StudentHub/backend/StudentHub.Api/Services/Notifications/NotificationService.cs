using Microsoft.EntityFrameworkCore;
using StudentHub.Core.Entities.Notifications;
using StudentHub.Infrastructure.Data;

namespace StudentHub.Api.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly StudentHubDbContext _context;

        public NotificationService(StudentHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetForUserAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<bool> MarkAsReadAsync(int id)
        {
            var n = await _context.Notifications.FindAsync(id);
            if (n == null) return false;

            n.IsRead = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
