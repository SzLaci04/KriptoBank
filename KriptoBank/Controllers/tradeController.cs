using Microsoft.AspNetCore.Mvc;
using KriptoBank.Services.Services;
using KriptoBank.DataContext.Dtos;

namespace KriptoBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class tradeController : Controller
    {
        private ITradeServices _tradeService;
        public tradeController(ITradeServices tradeService)
        {
            _tradeService = tradeService;
        }
        [HttpPost("buy")]
        public async Task<IActionResult> CryptoBuy(TransactionBuyDto buyDto)
        {
            var success = await _tradeService.BuyCryptoAsync(buyDto);
            if (success!=null)
                return Ok(success);
            return BadRequest("Nincs ilyen felhasználó vagy pénztárcája vagy kriptovaluta vagy nincs elég fedezete!");
        }
        [HttpPost("sell")]
        public async Task<IActionResult> CryptoSell(TransactionSellDto sellDto)
        {
            var success = await _tradeService.SellCryptoAsync(sellDto);
            if (success!=null)
                return Ok(success);
            return BadRequest("Nincs ilyen felhasználó vagy pénztárcája vagy kriptovaluta vagy túl sokat szeretne eladni!");
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class portfolioController:ControllerBase
    {
        private ITradeServices _tradeService;
        public portfolioController(ITradeServices tradeService)
        {
            _tradeService = tradeService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPortfolio(int userId)
        {
            var profit = await _tradeService.GetPortfolioAsync(userId);
            if (profit != null)
                return Ok(profit);
            return NotFound("Nincs ilyen felhasználó vagy pénztárcája!");
        }
    }
}
