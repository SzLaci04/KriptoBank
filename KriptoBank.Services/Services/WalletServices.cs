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
            var wallet = await _appDbContext.Wallets.FirstOrDefaultAsync(w=>w.UserId==userId);
            if (wallet == null||wallet.IsDeleted)
                return false;
            wallet.IsDeleted = true;
            _appDbContext.Wallets.Update(wallet);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<WalletCurrentStateDto> GetWalletAsync(int userId)
        {
            var wallet = await _appDbContext.Wallets.Include(w=>w.UserCurrencies).FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null || wallet.IsDeleted)
                return null;
            return _mapper.Map<WalletCurrentStateDto>(wallet);
        }

        public async Task<WalletCurrentStateDto?> UpdateBalanceAsync(int userId, WalletUpdateDto newBalance)
        {
            var Wallet = await _appDbContext.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (Wallet == null || Wallet.IsDeleted)
                return null;
            Wallet.Balance=newBalance.Balance;
            _appDbContext.Wallets.Update(Wallet);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<WalletCurrentStateDto>(Wallet);

        }
    }
}
