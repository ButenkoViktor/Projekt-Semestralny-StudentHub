using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace StudentHub.Api.Hubs
{
    [Authorize] 
    public class ChatHub : Hub
    {
        public async Task JoinRoom(int roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        public async Task LeaveRoom(int roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        public async Task SendMessageToRoom(int roomId, object messagePayload)
        {
            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", messagePayload);
        }

        public override Task OnConnectedAsync() => base.OnConnectedAsync();
        public override Task OnDisconnectedAsync(Exception? exception) => base.OnDisconnectedAsync(exception);
    }
}