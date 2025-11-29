using System.Collections.Generic;
namespace StudentHub.Core.Entities.Chat
{
    public class ChatRoom

    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public ICollection<ChatParticipant>? Participants { get; set; }
        public ICollection<ChatMessage>? Messages { get; set; }
    }
}