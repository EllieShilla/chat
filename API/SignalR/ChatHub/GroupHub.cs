using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR.ChatHub
{
    public class GroupHub : Hub<IChatClient>
    {
        public async Task JoinChat(ForConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatTitle);

            await Clients.Group(connection.ChatTitle).ReceivedMessageAsync("Admin", $"{connection.UserName} joined to chat");
        }
    }
}

