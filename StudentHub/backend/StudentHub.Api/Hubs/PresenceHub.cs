using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

[Authorize]
public class PresenceHub : Hub
{

    public static ConcurrentDictionary<string, int> OnlineUsers = new();

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!string.IsNullOrEmpty(userId))
        {
            OnlineUsers.AddOrUpdate(
                userId,
                1,
                (key, old) => old + 1
            );

            await Clients.All.SendAsync("UserOnline", userId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!string.IsNullOrEmpty(userId) && OnlineUsers.ContainsKey(userId))
        {
            OnlineUsers[userId]--;

            if (OnlineUsers[userId] <= 0)
            {
                OnlineUsers.TryRemove(userId, out _);

                await Clients.All.SendAsync("UserOffline", userId);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }
}