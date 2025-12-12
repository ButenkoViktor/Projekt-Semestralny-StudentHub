namespace StudentHub.Api.Models.Chat
{
    public class SendMessageRequest
    {
        public int RoomId { get; set; }
        public string Content { get; set; } = default!;
    }
}
