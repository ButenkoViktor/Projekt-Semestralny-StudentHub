namespace StudentHub.Api.Models.Notifications
{
    public class CreateNotificationDto
    {
        public string UserId { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
