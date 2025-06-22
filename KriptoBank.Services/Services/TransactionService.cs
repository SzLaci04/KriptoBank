using AutoMapper;
using KriptoBank.DataContext.Context;
using KriptoBank.DataContext.Dtos;
using KriptoBank.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.Services.Services
{
    public interface ITransactionService
    {
        public Task<List<TransactionShortDto>> GetTransactionsAsync(int userId);
        public Task<TransactionDto> GetTransactionDetailAsync(int transactionId);
    }
    public class TransactionService : ITransactionService
    {
        private AppDbContext _appDbContext;
        private IMapper _mapper;
        public TransactionService(AppDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }

        public async Task<TransactionDto> GetTransactionDetailAsync(int transactionId)
        {
            var transcations = await _appDbContext.Transactions.FindAsync(transactionId);
            if (transcations == null)
                return null;
            return _mapper.Map<TransactionDto>(transcations);
        }

        public async Task<List<TransactionShortDto>> GetTransactionsAsync(int userId)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            if (user == null||user.IsDeleted)
                return null;
            var transcations = await _appDbContext.Transactions.Where(t => t.UserId == userId).ToListAsync();
            return _mapper.Map<List<TransactionShortDto>>(transcations);
        }
    }
}
