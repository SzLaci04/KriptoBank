using Microsoft.AspNetCore.Mvc;
using KriptoBank.Services.Services;
using KriptoBank.DataContext.Dtos;

namespace KriptoBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class profitController : Controller
    {
        private readonly IProfitService _ProfitService;
        public profitController(IProfitService profitService)
        {
            _ProfitService = profitService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfits(int userId)
        {
            var profits = await _ProfitService.GetUserProfitsAsync(userId);
            if (profits != null)
                return Ok(profits);
            return NotFound("Nincs ilyen id-vel ellátott felhasználó vagy nincs pénztárcája!");
        }
        [HttpGet("details/{userId}")]
        public async Task<IActionResult> GetUserProfitsDetails(int userId)
        {
            var profits = await _ProfitService.GetUserProfitsDetailAsync(userId);
            if (profits != null)
                return Ok(profits);
            return NotFound("Nincs ilyen id-vel ellátott felhasználó vagy nincs pénztárcája!");
        }
    }
}
