namespace StudentHub.Api.Models.Chat
{
    public class ChatRoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public IEnumerable<ChatMessageDto>? Messages { get; set; }
    }
}
