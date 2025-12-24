using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace StudentHub.Api.Hubs
{
    [Authorize]
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task JoinRoom(int roomId)
            => await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());

        public async Task SendToRoom(int roomId, object message)
            => await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", message);
    }
}