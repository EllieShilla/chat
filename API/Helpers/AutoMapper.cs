using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using API.Entities;

namespace API.Helpers
{
    public static class AutoMapper
    {
        public static AnalyzedMessageDto FromMessageToAnalyzedMessageDto(Message message)
        {

            AnalyzedMessageDto analyzedMessage = new AnalyzedMessageDto()
            {
                Text = message.Text,
                Timestamp = message.Timestamp,
                UserName = message.AppUser.UserName,
                Sentiment = message.Sentiment
            };

            return analyzedMessage;
        }
    }
}