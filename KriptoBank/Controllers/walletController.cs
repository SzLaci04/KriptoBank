using Microsoft.AspNetCore.Mvc;
using KriptoBank.Services.Services;

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
            return wallet is null ? NotFound() : Ok(wallet);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateBalance(int userId, [FromBody] float newBalance)
        {
            var success = await _walletService.UpdateBalanceAsync(userId, newBalance);
            return Ok(success);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteWallet(int userId)
        {
            var success = await _walletService.DeleteWalletAsync(userId);
            return success ? NoContent() : NotFound();
        }
    }
}