using API.Dto;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUserDto>> Register(AppUserDto appUserDto)
        {
            if (await _userRepository.GetUserByUserNameAsync(appUserDto.UserName) == null && await _userRepository.GetUserByEmailAsync(appUserDto.Email) == null)
            {

                var user = new AppUser
                {
                    UserName = appUserDto.UserName,
                    Email = appUserDto.Email
                };

                _userRepository.AddUser(user);
                await _userRepository.SaveAllAsync();

                return Ok(appUserDto);
            }

            return BadRequest("Username is taken");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(AppUserDto appUserDto)
        {
            if (await _userRepository.GetUserByEmailAndUserName(appUserDto) != null)
            {
                return Ok(appUserDto);
            }

            return BadRequest("Invalid User");
        }
    }
}



