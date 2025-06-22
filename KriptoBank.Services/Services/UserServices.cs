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
    public interface IUserServices
    {
        Task<UserDataDto> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
        Task<UserDataDto> GetUserByIdAsync(int userId);
        Task<UserDataDto> UpdateUserPasswordAsync(int userId, UserUpdatePasswordDto userUpdate);
        Task<bool>DeleteUserAsync(int userId);
    }
    public class UserServices : IUserServices
    {
        private AppDbContext _appDbContext;
        private IMapper _mapper;
        public UserServices(AppDbContext context,IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user=await _appDbContext.Users.FindAsync(userId);
            if (user == null)
                return false;
            user.IsDeleted = true;

            //sell cryptos in wallet at price bought and then delete wallet, user
            var wallet = await _appDbContext.Wallets.Include(w => w.UserCurrencies).FirstOrDefaultAsync(w => w.UserId == userId);
            foreach (var uc in wallet.UserCurrencies)
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
                await _appDbContext.SaveChangesAsync();
            }
            //delete wallet
            if (wallet != null||!wallet.IsDeleted)
            {
                wallet.IsDeleted = true;
                _appDbContext.Wallets.Update(wallet);
            }
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserDataDto> GetUserByIdAsync(int userId)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            if (user==null || user.IsDeleted)
                return null;
            return _mapper.Map<UserDataDto>(user);
        }

        public async Task<UserDataDto> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            var user=_mapper.Map<User>(userRegistrationDto);
            //check for unique email addresses
            var allUsers= _appDbContext.Users.ToList();
            foreach(var _user in allUsers.Where(u=>!u.IsDeleted))
            {
                if (_user.Email == user.Email)
                    return new UserDataDto { Username = "NotUnique", Email = "NotUnique", Id = -1 };
            }
            //add user
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            //add wallet
            Wallet wallet = new Wallet { UserId = user.Id , Balance=4000.00F};
            await _appDbContext.Wallets.AddAsync(wallet);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<UserDataDto>(user);
        }

        public async Task<UserDataDto> UpdateUserPasswordAsync(int userId, UserUpdatePasswordDto userUpdate)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            if ( user==null|| user.IsDeleted)
                return null;
            _mapper.Map(userUpdate,user);
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<UserDataDto>(user);
        }
    }
}