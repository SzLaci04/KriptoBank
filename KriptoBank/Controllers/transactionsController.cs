using Microsoft.AspNetCore.Mvc;
using KriptoBank.Services.Services;
using KriptoBank.DataContext.Dtos;

namespace KriptoBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class transactionsController : Controller
    {
        private readonly ITransactionService _TransactionService;
        public transactionsController(ITransactionService TransactionService)
        {
            _TransactionService = TransactionService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTransaction(int userId)
        {
            var transactions= await _TransactionService.GetTransactionsAsync(userId);
            if (transactions != null)
                return Ok(transactions);
            return NotFound("Nincs ilyen id-vel ellátott felhasználó!");
        }
        [HttpGet("details/{transactionId}")]
        public async Task<IActionResult> GetTransactionDetail(int transactionId)
        {
            var transactionDetail = await _TransactionService.GetTransactionDetailAsync(transactionId);
            if (transactionDetail != null)
                return Ok(transactionDetail);
            return NotFound("Nincs ilyen id-vel ellátott tranzakció!");
        }
    }
}
