using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StudentHub.Api.Models.Chat;
using StudentHub.Api.Hubs;
using StudentHub.Core.Entities.Chat;
using StudentHub.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub.Api.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly StudentHubDbContext _context;
        private readonly IHubContext<ChatHub> _hub;

        public ChatService(StudentHubDbContext context, IHubContext<ChatHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public async Task<IEnumerable<ChatRoomDto>> GetRoomsAsync(string userId)
        {
            return await _context.ChatParticipants
                .Where(cp => cp.UserId == userId)
                .Select(cp => new ChatRoomDto
                {
                    Id = cp.ChatRoomId,
                    Name = cp.ChatRoom.Name
                })
                .ToListAsync();
        }

        public async Task<ChatRoomDto?> GetRoomAsync(int roomId)
        {
            var room = await _context.ChatRooms
                .Include(r => r.Messages)
                .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null) return null;

            var messages = room.Messages?
                .OrderBy(m => m.SentAt)
                .Select(m => new ChatMessageDto
                {
                    Id = m.Id,
                    RoomId = m.ChatRoomId,
                    UserId = m.UserId,
                    UserName = $"{m.User.FirstName} {m.User.LastName}",
                    Content = m.Content,
                    SentAt = m.SentAt
                });

            return new ChatRoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Messages = messages
            };
        }

        public async Task<ChatMessageDto> SendMessageAsync(string userId, SendMessageRequest request)
        {
            // зберігаємо в БД
            var message = new ChatMessage
            {
                ChatRoomId = request.RoomId,
                UserId = userId,
                Content = request.Content,
                SentAt = DateTime.UtcNow
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            // отримаємо дані користувача для імені
            var user = await _context.Users.FindAsync(userId);
            var userName = user != null ? $"{user.FirstName} {user.LastName}" : userId;

            var dto = new ChatMessageDto
            {
                Id = message.Id,
                RoomId = message.ChatRoomId,
                UserId = userId,
                UserName = userName,
                Content = message.Content,
                SentAt = message.SentAt
            };

            await _hub.Clients.Group(request.RoomId.ToString())
                .SendAsync("ReceiveMessage", dto);

            return dto;
        }
    }
}