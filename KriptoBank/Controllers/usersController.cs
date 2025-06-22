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
                return NotFound("Nincs ilyen id-vel ellátott felhasználó");
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistrationDto)
        {
            var user= await _userServices.RegisterUserAsync(userRegistrationDto);
            if (user.Email == "NotUnique")
                return BadRequest("Ilyen email címmel vagy felhasználónévvel már csináltak felhasználót!");
            return CreatedAtAction(nameof(GetUserData),new { id=user.Id},user);
                
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateUser(int id,UserUpdatePasswordDto updatePasswordDto)
        {
            var user = await _userServices.UpdateUserPasswordAsync(id, updatePasswordDto);
            if (user == null)
                return NotFound("Nincs ilyen id-vel ellátott felhasználó vagy érvénytelen felhasználónév/emailcím");
            return Ok(user);
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult>DeleteUser(int userId)
        {
            var success=await _userServices.DeleteUserAsync(userId);
            if (success)
                return Ok($"{userId} id-vel ellátott felhasználó törölve!");
            else
                return NotFound("Nincs ilyen id-vel ellátott felhasználó");
        }
    }
}
