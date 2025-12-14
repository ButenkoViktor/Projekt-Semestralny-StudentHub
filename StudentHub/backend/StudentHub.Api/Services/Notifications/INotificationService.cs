using StudentHub.Core.Entities.Notifications;

public interface INotificationService
{
    Task<IEnumerable<Notification>> GetForUserAsync(string userId);
    Task<Notification> CreateAsync(Notification notification);
    Task<bool> MarkAsReadAsync(int id);
}