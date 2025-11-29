using System;
using StudentHub.Core.Entities.Users;

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
        /// <summary>
        /// Gets or sets the unique identifier for the user associated with this instance. Foreign key referencing the User entity.
        /// </summary>
        public string UserId { get; set; } = default!;
        /// <summary>
        /// Gets or sets the user associated with the current context.
        /// </summary>
        public User User { get; set; } = default!;
        /// <summary>
        /// Gets or sets the textual content associated with this instance.
        /// </summary>
        public string Content { get; set; } = default!;
        /// <summary>
        /// Gets or sets the date and time, in UTC, when the message was sent.
        /// </summary>
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}