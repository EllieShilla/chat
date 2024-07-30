using API.Dto;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> SaveAllAsync();
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<AppUser> GetUserByUserNameAsync(string userName);
        void AddUser(AppUser user);
        Task<AppUser> GetUserByEmailAndUserName(AppUserDto user);

    }
}