using System;
using StudentHub.Core.Entities.Users;

namespace StudentHub.Core.Entities.Chat
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}