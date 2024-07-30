using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using API.Entities;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        Task<bool> SaveAllAsync();
        void SaveMessage(Message message);
        Task<IEnumerable<Message>> GetAllMessages();
    }
}