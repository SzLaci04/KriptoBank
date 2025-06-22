using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KriptoBank.DataContext.Context;
using KriptoBank.DataContext.Dtos;
using KriptoBank.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace KriptoBank.Services.Services
{
    public interface IWalletServices
    {
        public Task<WalletCurrentStateDto> GetWalletAsync(int userId);
        public Task<WalletCurrentStateDto?> UpdateBalanceAsync(int userId, WalletUpdateDto newBalance);
        public Task<bool> DeleteWalletAsync(int userId);
    }
    public class WalletServices : IWalletServices
    {
        private AppDbContext _appDbContext;
        private IMapper _mapper;
        public WalletServices(AppDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }
        public async Task<bool> DeleteWalletAsync(int userId)
        {
            var wallet = await _appDbContext.Wallets.Include(w=>w.UserCurrencies).FirstOrDefaultAsync(w=>w.UserId==userId);
            if (wallet == null||wallet.IsDeleted)
                return false;
            wallet.IsDeleted = true;
            _appDbContext.Wallets.Update(wallet);
            //sell all cryptos inside at price bought and then delete wallet
            var uCurrencies = wallet.UserCurrencies.ToList();
            foreach (var uc in uCurrencies)
            {
                var sell = new CryptoTransaction { UserId = userId, CryptoId = uc.CryptoId, Amount = uc.Amount };
                sell.TimeOfTransaction = DateTime.Now;
                sell.Type = TransactionType.sell;
                //check if crypto exists
                var crypto = await _appDbContext.CryptoCurrencies.FindAsync(sell.CryptoId);
                if (crypto == null || crypto.IsDeleted)
                    continue;
                sell.Price = uc.PriceAtBuy;
                sell.TotalPrice = sell.Amount * sell.Price;
                //update crypto
                crypto.TotalAmount += sell.Amount;
                _appDbContext.CryptoCurrencies.Update(crypto);
                await _appDbContext.SaveChangesAsync();
                //add transaction
                await _appDbContext.Transactions.AddAsync(sell);
                //remove usercurrency from wallet
                wallet.UserCurrencies.Remove(uc);
                await _appDbContext.SaveChangesAsync();
            }
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<WalletCurrentStateDto> GetWalletAsync(int userId)
        {
            var cryptos = await _appDbContext.CryptoCurrencies.ToListAsync();

            var wallet = await _appDbContext.Wallets.Include(w=>w.UserCurrencies).FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null || wallet.IsDeleted)
                return null;
            var userCurrencies = wallet.UserCurrencies.ToList();
            foreach(var uc in userCurrencies)
            {
                var crypto = cryptos.FirstOrDefault(c => c.Id == uc.CryptoId);
                if(crypto==null||crypto.IsDeleted)
                {
                    wallet.UserCurrencies.Remove(uc);
                }
            }
            
            return _mapper.Map<WalletCurrentStateDto>(wallet);
        }

        public async Task<WalletCurrentStateDto?> UpdateBalanceAsync(int userId, WalletUpdateDto newBalance)
        {
            var Wallet = await _appDbContext.Wallets.Include(w=>w.UserCurrencies).FirstOrDefaultAsync(w => w.UserId == userId);
            if (Wallet == null || Wallet.IsDeleted)
                return null;
            Wallet.Balance=newBalance.Balance;
            _appDbContext.Wallets.Update(Wallet);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<WalletCurrentStateDto>(Wallet);

        }
    }
}
