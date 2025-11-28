using StudentHub.Core.Entities.Users;
public class ChatParticipant
{
    public int Id { get; set; }
    public int ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}