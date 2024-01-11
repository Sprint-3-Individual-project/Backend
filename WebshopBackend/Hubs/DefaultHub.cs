using Microsoft.AspNetCore.SignalR;

namespace WebshopBackend.Hubs
{
    public class DefaultHub : Hub
    {
        public Task BroadcastMessage(string message) =>
            Clients.All.SendAsync("broadcastMessage",message);

        public Task Echo(string name, string message) =>
            Clients.Client(Context.ConnectionId)
                    .SendAsync("echo", name, $"{message} (echo from server)");
    }
}
