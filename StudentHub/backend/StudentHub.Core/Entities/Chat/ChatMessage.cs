using System;
using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Chat
{
    public class ChatMessage
    {

        public int Id { get; set; }

        public int ChatRoomId { get; set; }

        public ChatRoom ChatRoom { get; set; } = default!;

        public string SenderId { get; set; } = default!;
        public ApplicationUser Sender { get; set; } = default!;

        public string Content { get; set; } = default!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}