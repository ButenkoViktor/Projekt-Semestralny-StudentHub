using StudentHub.Api.Models.Chat;

namespace StudentHub.Api.Services.Chat
{
    public interface IChatService
    {
        Task<IEnumerable<ChatRoomDto>> GetRoomsAsync(string userId);
        Task<ChatRoomDto?> GetRoomAsync(int roomId, string userId);
        Task<ChatRoomDto> CreateOrGetRoomAsync(string userId, string targetUserId);
        Task<ChatMessageDto> SendMessageAsync(string userId, SendMessageRequest request);
        Task<IEnumerable<UserSearchDto>> SearchUsersAsync(string query, string currentUserId);
    }
}
