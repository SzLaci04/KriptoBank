using Microsoft.AspNetCore.Mvc;
using KriptoBank.DataContext.Entities;
using KriptoBank.DataContext.Dtos;
using KriptoBank.Services;
using KriptoBank.Services.Services;

namespace KriptoBank.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class usersController : ControllerBase
    {
        private IUserServices _userServices;

        public usersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserData(int id)
        { 
            var user=await _userServices.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistrationDto)
        {
            var user= await _userServices.RegisterUserAsync(userRegistrationDto);
            return CreatedAtAction(nameof(GetUserData),new { id=user.Id},user);
                
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateUser(int id,UserUpdatePasswordDto updatePasswordDto)
        {
            var user = await _userServices.UpdateUserPasswordAsync(id, updatePasswordDto);
            if (user == null) 
                return NotFound();
            return Ok(user);
        }
    }
}
