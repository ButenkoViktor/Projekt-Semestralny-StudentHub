namespace StudentHub.Api.Models.Chat
{
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime SentAt { get; set; }
    }
}
