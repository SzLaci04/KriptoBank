﻿using Microsoft.AspNetCore.Mvc;
using KriptoBank.Services.Services;
using KriptoBank.DataContext.Dtos;

namespace KriptoBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class cryptosController : ControllerBase
    {
        private readonly ICryptoServices _CryptoService;
        public cryptosController(ICryptoServices cryptoService)
        {
            _CryptoService = cryptoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCryptos()
        {
            var cryptos = await _CryptoService.GetAllCryptosAsync();
            return Ok(cryptos);
        }
        [HttpGet("{cryptoId}")]
        public async Task<IActionResult> GetSingleCrypto(int cryptoId)
        {
            var crypto = await _CryptoService.GetCryptoAsync(cryptoId);
            if (crypto!=null)
                return Ok(crypto);
            return NotFound("Nincs ilyen id-vel ellátott kriptovaluta!");
        }
        [HttpPost]
        public async Task<IActionResult> CreateCryptoCurrency(CryptoCurrencyCreateDto CreateCrypto)
        {
            var newCrypto = await _CryptoService.CreateCryptoAsnyc(CreateCrypto);
            if (newCrypto!=null)
                return Ok(newCrypto);
            return BadRequest("A kriptovaluta létrehozása nem sikerült!");
        }
        [HttpDelete("{cryptoId}")]
        public async Task<IActionResult> DeleteCryptoCurrency(int cryptoId)
        {
            var success = await _CryptoService.DeleteCryptoAsync(cryptoId);
            if (success)
                return Ok($"{cryptoId} id-vel ellátott kriptovaluta törölve!");
            return NotFound("Nincs ilyen id-vel ellátott kriptovaluta!");
        }
    }
}
