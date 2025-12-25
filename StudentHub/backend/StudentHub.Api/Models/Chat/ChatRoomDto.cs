namespace StudentHub.Api.Models.Chat
{
    public class ChatRoomDto
    {
        public int Id { get; set; }
        public string OtherUserId { get; set; } = default!;
        public string OtherUserName { get; set; } = default!;
        public IEnumerable<ChatMessageDto>? Messages { get; set; }
        public bool IsOnline { get; set; }
    }
}
