using System.Collections.Generic;
namespace StudentHub.Core.Entities.Chat
{
    public class ChatRoom
    {
        /// <summary>
        /// Primary key of the chat room.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the collection of participants associated with this chat room.
        /// </summary>
        public ICollection<ChatParticipant>? Participants { get; set; }
        /// <summary>
        /// Gets or sets the collection of chat messages associated with the conversation.
        /// </summary>
        /// <remarks>The collection may be null if no messages have been loaded or assigned. Modifying the
        /// collection does not automatically persist changes; ensure that updates are saved as appropriate for the
        /// application's data model.</remarks>
        public ICollection<ChatMessage>? Messages { get; set; }
    }
}