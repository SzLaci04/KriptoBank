using KriptoBank.DataContext.Dtos;
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
    public interface ITradeServices
    {
        public Task<TransactionDto> BuyCryptoAsync(TransactionBuyDto buyDto);
        public Task<TransactionDto> SellCryptoAsync(TransactionSellDto sellDto);
        public Task<PortfolioDto> GetPortfolioAsync(int userId);
    }
    public class TradeServices : ITradeServices
    {
        private AppDbContext _appDbContext;
        private IMapper _mapper;
        public TradeServices(AppDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }
        public async Task<TransactionDto> BuyCryptoAsync(TransactionBuyDto buyDto)
        {
            var buy=_mapper.Map<CryptoTransaction>(buyDto);
            buy.TimeOfTransaction = DateTime.Now;
            buy.Type=TransactionType.buy;
            //check if crypto exists
            var crypto = await _appDbContext.CryptoCurrencies.FindAsync(buy.CryptoId);
            if (crypto == null || crypto.IsDeleted)
                return null;
            buy.Price = crypto.CurrentPrice;
            buy.TotalPrice=buy.Amount*buy.Price;
            //check if wallet exists
            var wallet= await _appDbContext.Wallets.Include(w=>w.UserCurrencies).FirstOrDefaultAsync(w=>w.UserId==buy.UserId);
            if (wallet == null || wallet.IsDeleted)
                return null;
            //check if wallet has enough to buy and there is enough crypto available
            if (wallet.Balance>=buy.TotalPrice&&crypto.TotalAmount>0)
            {
                //update wallet
                //check if user has already bought this crypto before and increase that, else add to it
                if (wallet.UserCurrencies.FirstOrDefault(uc => uc.CryptoId == crypto.Id)!=null)
                    wallet.UserCurrencies.FirstOrDefault(uc=>uc.CryptoId==crypto.Id).Amount+=buy.Amount;
                else
                    wallet.UserCurrencies.Add(new UserCryptoCurrency { WalletId = wallet.Id, CryptoId = crypto.Id, PriceAtBuy = crypto.CurrentPrice, Amount = buy.Amount });
                wallet.Balance-=buy.TotalPrice;
                _appDbContext.Wallets.Update(wallet);
                await _appDbContext.SaveChangesAsync();
                //update crypto
                crypto.TotalAmount -= buy.Amount;
                _appDbContext.CryptoCurrencies.Update(crypto);
                //add transaction
                await _appDbContext.SaveChangesAsync();
                _appDbContext.Transactions.AddAsync(buy);
                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<TransactionDto>(buy);
            }
            return null;
        }

        public async Task<PortfolioDto> GetPortfolioAsync(int userId)
        {
            var wallet=await _appDbContext.Wallets.Include(w => w.UserCurrencies).FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null|| wallet.IsDeleted)
                return null;
            var cryptos = await _appDbContext.CryptoCurrencies.Where(c => !c.IsDeleted).ToListAsync();
            var portfolio = new PortfolioDto
            {
                UserId = userId,
                BaseBalance = wallet.Balance,
                userCryptoCurrencies = _mapper.Map<List<UserCryptoCurrencyDto>>(wallet.UserCurrencies.Where(uc=>cryptos.FirstOrDefault(c=>c.Id==uc.CryptoId)!=null)),
                TotalBalance = wallet.Balance
            };
            var walletid = wallet.Id;
            float cryptoBalance = 0f;
            foreach (var uc in portfolio.userCryptoCurrencies)
            {
                var crypto=cryptos.FirstOrDefault(c => c.Id == uc.CryptoId);
                if (crypto != null)
                {
                    cryptoBalance += uc.Amount * crypto.CurrentPrice;
                }
            }
            portfolio.TotalBalance += cryptoBalance;
            return portfolio;
        }

        public async Task<TransactionDto> SellCryptoAsync(TransactionSellDto sellDto)
        {
            var sell = _mapper.Map<CryptoTransaction>(sellDto);
            sell.TimeOfTransaction = DateTime.Now;
            sell.Type = TransactionType.sell;
            //check if crypto exists
            var crypto = await _appDbContext.CryptoCurrencies.FindAsync(sell.CryptoId);
            if (crypto == null || crypto.IsDeleted)
                return null;
            sell.Price = crypto.CurrentPrice;
            sell.TotalPrice = sell.Amount * sell.Price;
            //check if wallet exists
            var wallet = await _appDbContext.Wallets.Include(w => w.UserCurrencies).FirstOrDefaultAsync(w => w.UserId == sell.UserId);
            if (wallet == null || wallet.IsDeleted)
                return null;
            //check if user has enough crypto to sell
            var userCrypto = wallet.UserCurrencies.FirstOrDefault(uc => uc.CryptoId == crypto.Id);
            if (userCrypto == null || userCrypto.Amount < sell.Amount)
                return null;
            //update wallet
            userCrypto.Amount -= sell.Amount;
            if (userCrypto.Amount == 0)
            {
                wallet.UserCurrencies.Remove(userCrypto);
            }
            wallet.Balance += sell.TotalPrice;
            _appDbContext.Wallets.Update(wallet);
            await _appDbContext.SaveChangesAsync();
            //update crypto
            crypto.TotalAmount += sell.Amount;
            _appDbContext.CryptoCurrencies.Update(crypto);
            await _appDbContext.SaveChangesAsync();
            //add transaction
            await _appDbContext.Transactions.AddAsync(sell);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<TransactionDto>(sell);
        }
    }
}
