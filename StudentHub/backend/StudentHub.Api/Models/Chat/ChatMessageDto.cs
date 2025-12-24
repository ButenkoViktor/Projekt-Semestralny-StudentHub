namespace StudentHub.Api.Models.Chat
{
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime SentAt { get; set; }
    }
}
