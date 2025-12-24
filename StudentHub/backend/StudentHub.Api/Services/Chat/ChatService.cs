using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentHub.Api.Models.Chat;
using StudentHub.Api.Services.Chat;
using StudentHub.Core.Entities.Chat;
using StudentHub.Core.Entities.Identity;
using StudentHub.Infrastructure.Data;

public class ChatService : IChatService
{
    private readonly StudentHubDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ChatService(StudentHubDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserSearchDto>> SearchUsersAsync(string query, string currentUserId)
    {
        return await _userManager.Users
            .Where(u => u.Id != currentUserId &&
                        (u.Email!.Contains(query) ||
                         (u.FirstName + " " + u.LastName).Contains(query)))
            .Select(u => new UserSearchDto
            {
                Id = u.Id,
                Email = u.Email!,
                FullName = u.FirstName + " " + u.LastName
            })
            .Take(10)
            .ToListAsync();
    }

    public async Task<ChatRoomDto> CreateOrGetRoomAsync(string userId, string targetUserId)
    {
        var room = await _db.ChatRooms.FirstOrDefaultAsync(r =>
            (r.User1Id == userId && r.User2Id == targetUserId) ||
            (r.User1Id == targetUserId && r.User2Id == userId));

        if (room == null)
        {
            room = new ChatRoom
            {
                User1Id = userId,
                User2Id = targetUserId
            };
            _db.ChatRooms.Add(room);
            await _db.SaveChangesAsync();
        }

        return await MapRoom(room, userId);
    }

    public async Task<IEnumerable<ChatRoomDto>> GetRoomsAsync(string userId)
    {
        var rooms = await _db.ChatRooms
            .Where(r => r.User1Id == userId || r.User2Id == userId)
            .ToListAsync();

        var result = new List<ChatRoomDto>();
        foreach (var room in rooms)
            result.Add(await MapRoom(room, userId));

        return result;
    }

    public async Task<ChatRoomDto?> GetRoomAsync(int roomId, string userId)
    {
        var room = await _db.ChatRooms
            .Include(r => r.Messages)
            .FirstOrDefaultAsync(r => r.Id == roomId);

        if (room == null) return null;
        if (room.User1Id != userId && room.User2Id != userId) return null;

        return await MapRoom(room, userId, true);
    }

    public async Task<ChatMessageDto> SendMessageAsync(string userId, SendMessageRequest request)
    {
        var message = new ChatMessage
        {
            ChatRoomId = request.RoomId,
            SenderId = userId,
            Content = request.Content
        };

        _db.ChatMessages.Add(message);
        await _db.SaveChangesAsync();

        return new ChatMessageDto
        {
            Id = message.Id,
            SenderId = userId,
            Content = message.Content,
            SentAt = message.SentAt
        };
    }

    private async Task<ChatRoomDto> MapRoom(ChatRoom room, string currentUserId, bool withMessages = false)
    {
        var otherId = room.User1Id == currentUserId ? room.User2Id : room.User1Id;
        var user = await _userManager.FindByIdAsync(otherId);

        return new ChatRoomDto
        {
            Id = room.Id,
            OtherUserId = otherId,
            OtherUserName = $"{user!.FirstName} {user.LastName}",
            Messages = withMessages
                ? room.Messages
                    .OrderBy(m => m.SentAt)
                    .Select(m => new ChatMessageDto
                    {
                        Id = m.Id,
                        SenderId = m.SenderId,
                        Content = m.Content,
                        SentAt = m.SentAt
                    })
                : null
        };
    }
}