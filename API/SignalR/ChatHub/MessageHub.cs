using API.Dto;
using API.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR.ChatHub
{
    public class MessageHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        public MessageHub(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

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
