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
        /// Gets or sets the unique identifier of the first user in the relationship.
        /// </summary>
        public string User1Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the second user associated with the entity.
        /// </summary>
        public string User2Id { get; set; }

        /// <summary>
        /// Gets or sets the collection of chat messages associated with the chat session.
        /// </summary>
        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}