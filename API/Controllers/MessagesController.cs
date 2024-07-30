using API.Dto;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly TextAnalyticsService _textAnalyticsService;

        public MessagesController(IMessageRepository messageRepository, IUserRepository userRepository, TextAnalyticsService textAnalyticsService)
        {
            _textAnalyticsService = textAnalyticsService;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        [HttpPost("savemessage")]
        public async Task<ActionResult<AnalyzedMessageDto>> PostMessage([FromBody] MessageDto chatMessage)
        {
            AppUser user = await _userRepository.GetUserByUserNameAsync(chatMessage.UserName);
            chatMessage.Timestamp = DateTime.UtcNow;
            var message = new Message
            {
                Text = chatMessage.Text,
                Timestamp = chatMessage.Timestamp,
                Sentiment = await _textAnalyticsService.AnalyzeSentimentAsync(chatMessage.Text),
                AppUser = user,
            };
            _messageRepository.SaveMessage(message);
            if (await _messageRepository.SaveAllAsync())
            {
                return Ok(AutoMapper.FromMessageToAnalyzedMessageDto(message));
            }

            return BadRequest("Problem adding message");
        }
    }
}