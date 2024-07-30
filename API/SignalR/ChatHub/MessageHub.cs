using API.Dto;
using API.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR.ChatHub
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(AnalyzedMessageDto messageDto)
        {
            await Clients.All.SendAsync("ReceiveMessage", messageDto);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
