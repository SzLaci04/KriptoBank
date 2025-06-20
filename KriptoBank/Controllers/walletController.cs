using Microsoft.AspNetCore.Mvc;
using KriptoBank.Services.Services;
using KriptoBank.DataContext.Dtos;

namespace KriptoBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class walletController : ControllerBase
    {
        private readonly IWalletServices _walletService;
        public walletController(IWalletServices walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWallet(int userId)
        {
            var wallet = await _walletService.GetWalletAsync(userId);
            if (wallet != null)
                return Ok(wallet);
            return NotFound("Nincs ilyen felhasználó vagy nincs neki pénztárcája!");

        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateBalance(int userId, [FromBody] WalletUpdateDto newBalance)
        {
            var success = await _walletService.UpdateBalanceAsync(userId, newBalance);
            if (success!=null)
                return Ok(success);
            return NotFound("Nincs ilyen felhasználó vagy nincs neki pénztárcája!");
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteWallet(int userId)
        {
            var success = await _walletService.DeleteWalletAsync(userId);
            return success ? Ok($"{userId} felhasználó pénztárcája törölve!") : NotFound("Nincs ilyen felhasználó vagy nincs neki pénztárcája!");
        }
    }
}