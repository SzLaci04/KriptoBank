using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KriptoBank.DataContext.Context;
using KriptoBank.DataContext.Dtos;
using KriptoBank.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace KriptoBank.Services.Services
{
    public interface ICryptoServices
    {
        public Task<List<CryptoCurrencyDto>> GetAllCryptosAsync();
        public Task<CryptoCurrencyDto> GetCryptoAsync(int cryptoId);
        public Task<CryptoCurrencyDto> CreateCryptoAsnyc(CryptoCurrencyCreateDto CreateCrypto);
        public Task<bool> DeleteCryptoAsync(int cryptoId);
        public Task<CryptoCurrencyDto> ManualPriceChangeAsync(CryptoPriceUpdateDto priceUpdate);
        public Task<List<CryptoHistoryDto>> GetCryptoHistoryAsync(int cryptoId);
    }
    public class CryptoServices:ICryptoServices
    {
        private AppDbContext _appDbContext;
        private IMapper _mapper;
        public CryptoServices(AppDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }

        public async Task<CryptoCurrencyDto> CreateCryptoAsnyc(CryptoCurrencyCreateDto CreateCrypto)
        {
            var newCrypto=_mapper.Map<CryptoCurrency>(CreateCrypto);
            newCrypto.AvgPrice=CreateCrypto.StartPrice;
            await _appDbContext.CryptoCurrencies.AddAsync(newCrypto);
            await _appDbContext.SaveChangesAsync();
            var createdCrypto=await _appDbContext.CryptoCurrencies.FirstOrDefaultAsync(cc=>cc.Acronym==newCrypto.Acronym);
            if (createdCrypto == null)
                return null;
            return _mapper.Map<CryptoCurrencyDto>(createdCrypto);
        }

        public async Task<bool> DeleteCryptoAsync(int cryptoId)
        {
            var crypto = await _appDbContext.CryptoCurrencies.FindAsync(cryptoId);
            if (crypto == null||crypto.IsDeleted) 
                return false;

            //sell all cryptos at price bought and then delete crypto
            var wallets = await _appDbContext.Wallets.Include(w => w.UserCurrencies).ToListAsync();
            foreach (var wallet in wallets)
            {
                var userCrypto = wallet.UserCurrencies.FirstOrDefault(uc => uc.CryptoId == cryptoId);
                if (userCrypto != null)
                {
                    var sell = new CryptoTransaction
                    {
                        UserId = wallet.UserId,
                        CryptoId = cryptoId,
                        Amount = userCrypto.Amount,
                        Price = userCrypto.PriceAtBuy,
                        TotalPrice = userCrypto.Amount * userCrypto.PriceAtBuy,
                        TimeOfTransaction = DateTime.Now,
                        Type = TransactionType.sell
                    };
                    //update wallet
                    userCrypto.Amount -= sell.Amount;
                    if (userCrypto.Amount == 0)
                    {
                        wallet.UserCurrencies.Remove(userCrypto);
                    }
                    wallet.Balance += sell.TotalPrice;
                    _appDbContext.Wallets.Update(wallet);
                    await _appDbContext.SaveChangesAsync();
                    //add transaction
                    await _appDbContext.Transactions.AddAsync(sell);
                    await _appDbContext.SaveChangesAsync();
                    //remove from user's wallet
                    wallet.UserCurrencies.Remove(userCrypto);
                }
            }
            crypto.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<CryptoCurrencyDto>> GetAllCryptosAsync()
        {
            var allCryptos=await _appDbContext.CryptoCurrencies.Where(cc=>!cc.IsDeleted).ToListAsync();
            return _mapper.Map<List<CryptoCurrencyDto>>(allCryptos);
        }

        public async Task<CryptoCurrencyDto> GetCryptoAsync(int cryptoId)
        {
            var Crypto = await _appDbContext.CryptoCurrencies.FindAsync(cryptoId);
            if (Crypto == null||Crypto.IsDeleted)
                return null;
            return _mapper.Map<CryptoCurrencyDto>(Crypto);
        }

        public async Task<List<CryptoHistoryDto>> GetCryptoHistoryAsync(int cryptoId)
        {
            var crypto = await _appDbContext.CryptoCurrencies.FindAsync(cryptoId);
            var history= await _appDbContext.Histories.Where(h => h.CryptoId == cryptoId).OrderBy(h => h.TimeOfChange).ToListAsync();
            if (crypto == null || crypto.IsDeleted)
                return null;
            return _mapper.Map<List<CryptoHistoryDto>>(history);
        }

        public async Task<CryptoCurrencyDto> ManualPriceChangeAsync(CryptoPriceUpdateDto priceUpdate)
        {
            var crypto = await _appDbContext.CryptoCurrencies.FindAsync(priceUpdate.Id);
            if (crypto == null || crypto.IsDeleted)
                return null;
            var newhistory = new CryptoHistory
            {
                CryptoId = crypto.Id,
                OldPrice = crypto.CurrentPrice,
                CurrentPrice = priceUpdate.NewPrice,
                TimeOfChange = DateTime.Now,
            };
            crypto.CurrentPrice = priceUpdate.NewPrice;
            await _appDbContext.Histories.AddAsync(newhistory);
            await _appDbContext.SaveChangesAsync();
            var histories = await _appDbContext.Histories
                .Where(h => h.CryptoId == crypto.Id)
                .OrderByDescending(h => h.TimeOfChange)
                .ToListAsync();
            float avg = histories[0].OldPrice;
            foreach (var history in histories)
            {
                avg += history.CurrentPrice;
            }
            avg /= histories.Count + 1;
            crypto.AvgPrice = avg;
            _appDbContext.CryptoCurrencies.Update(crypto);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<CryptoCurrencyDto>(crypto);
        }
    }
}
