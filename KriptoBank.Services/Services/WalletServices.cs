using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KriptoBank.DataContext.Context;
using KriptoBank.DataContext.Dtos;
using KriptoBank.DataContext.Entities;

namespace KriptoBank.Services.Services
{
    public interface IWalletServices
    {
        public Task<WalletCurrentStateDto> GetWalletAsync(int userId);
        public Task<WalletUpdateDto> UpdateBalanceAsync(int userId, float newBalance);
        public Task<bool> DeleteWalletAsync(int userId);
    }
    public class WalletServices : IWalletServices
    {
        public Task<bool> DeleteWalletAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<WalletCurrentStateDto> GetWalletAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<WalletUpdateDto> UpdateBalanceAsync(int userId, float newBalance)
        {
            throw new NotImplementedException();
        }
    }
}
