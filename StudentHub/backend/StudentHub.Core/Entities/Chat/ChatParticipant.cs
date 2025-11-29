using System;
using StudentHub.Core.Entities.Users;
namespace StudentHub.Core.Entities.Chat
{
    public class ChatParticipant
    {
        /// <summary>
        /// Primary key of the chat participant.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier for the chat room. Foreign key referencing the ChatRoom entity.
        /// </summary>
        public int ChatRoomId { get; set; }
        /// <summary>
        /// Gets or sets the chat room associated with the current context.
        /// </summary>
        public ChatRoom ChatRoom { get; set; } = default!;
        /// <summary>
        /// Foreign key referencing the User entity.
        /// </summary>
        public string UserId { get; set; } = default!;
        /// <summary>
        /// Gets or sets the user associated with the current context.
        /// </summary>
        public User User { get; set; } = default!;
        /// <summary>
        /// Gets or sets the date and time at which the user joined.
        /// </summary>
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}