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
    public interface IProfitService
    {
        public Task<ProfitSummaryDto> GetUserProfitsAsync(int userId);
        public Task<ProfitDetailDto> GetUserProfitsDetailAsync(int userId);
    }
    public class ProfitService:IProfitService
    {
        private AppDbContext _appDbContext;
        private IMapper _mapper;
        public ProfitService(AppDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }

        public async Task<ProfitSummaryDto> GetUserProfitsAsync(int userId)
        {
            var wallet = await _appDbContext.Wallets.Include(w => w.UserCurrencies)
                .FirstOrDefaultAsync(w => w.UserId == userId && !w.IsDeleted);
            if (wallet == null)
                return null;
            var profits=new ProfitSummaryDto
            {
                UserId = userId,
                BaseValue = wallet.Balance,
                CurrentValue = wallet.Balance,
                TotalChange = 0
            };
            
            foreach (var userCrypto in wallet.UserCurrencies)
            {
                var crypto = await _appDbContext.CryptoCurrencies.FirstOrDefaultAsync(c => c.Id == userCrypto.CryptoId && !c.IsDeleted);
                if (crypto != null)
                {
                    profits.BaseValue += userCrypto.Amount * userCrypto.PriceAtBuy;
                    profits.CurrentValue += userCrypto.Amount * crypto.CurrentPrice;
                }
            }
            profits.TotalChange = profits.CurrentValue - profits.BaseValue;
            return profits;

        }

        public async Task<ProfitDetailDto> GetUserProfitsDetailAsync(int userId)
        {
            var wallet = await _appDbContext.Wallets.Include(w => w.UserCurrencies)
                .FirstOrDefaultAsync(w => w.UserId == userId && !w.IsDeleted);
            if (wallet == null)
                return null;
            var profits = new ProfitSummaryDto
            {
                UserId = userId,
                BaseValue = wallet.Balance,
                CurrentValue = wallet.Balance,
                TotalChange = 0
            };
            foreach (var userCrypto in wallet.UserCurrencies)
            {
                var crypto = await _appDbContext.CryptoCurrencies.FirstOrDefaultAsync(c => c.Id == userCrypto.CryptoId && !c.IsDeleted);
                if (crypto != null)
                {
                    profits.BaseValue += userCrypto.Amount * userCrypto.PriceAtBuy;
                    profits.CurrentValue += userCrypto.Amount * crypto.CurrentPrice;
                }
            }
            profits.TotalChange = profits.CurrentValue - profits.BaseValue;

            var cryptoChanges = new List<CryptoChangeDto>();
            foreach (var userCrypto in wallet.UserCurrencies)
            {
                var crypto = await _appDbContext.CryptoCurrencies.FirstOrDefaultAsync(c => c.Id == userCrypto.CryptoId && !c.IsDeleted);
                if (crypto != null)
                {
                    var change = new CryptoChangeDto
                    {
                        CryptoId = crypto.Id,
                        PriceAtBuy = userCrypto.PriceAtBuy,
                        CurrentPrice = crypto.CurrentPrice,
                        Amount = userCrypto.Amount,
                        AvgPrice=crypto.AvgPrice,
                        Change = (crypto.CurrentPrice - userCrypto.PriceAtBuy),
                        TotalChange=(crypto.CurrentPrice-userCrypto.PriceAtBuy)*userCrypto.Amount
                    };
                    cryptoChanges.Add(change);
                }
            }
            return new ProfitDetailDto { Summary=profits,CryptoChanges=cryptoChanges};

        }
    }
}
