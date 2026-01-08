using System.Collections.Generic;
namespace StudentHub.Core.Entities.Chat
{
    public class ChatRoom
    {

        public int Id { get; set; }
     
        public string User1Id { get; set; }

        public string User2Id { get; set; }

        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}