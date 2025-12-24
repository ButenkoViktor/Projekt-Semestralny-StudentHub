using System;
using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Chat
{
    public class ChatMessage
    {
        /// <summary>
        /// Primary key of the chat message.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Primary key of the associated chat room.
        /// </summary>
        public int ChatRoomId { get; set; }
        /// <summary>
        /// Gets or sets the chat room associated with this message.
        /// </summary>
        public ChatRoom ChatRoom { get; set; } = default!;

        public string SenderId { get; set; } = default!;
        public ApplicationUser Sender { get; set; } = default!;

        public string Content { get; set; } = default!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}