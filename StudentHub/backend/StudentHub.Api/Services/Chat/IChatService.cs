using StudentHub.Api.Models.Chat;

namespace StudentHub.Api.Services.Chat
{
    public interface IChatService
    {
        Task<IEnumerable<ChatRoomDto>> GetRoomsAsync(string userId);
        Task<ChatRoomDto?> GetRoomAsync(int roomId);
        Task<ChatMessageDto> SendMessageAsync(string userId, SendMessageRequest request);
    }
}
