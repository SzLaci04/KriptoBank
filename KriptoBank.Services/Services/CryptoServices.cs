﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public interface ICryptoServices
    {
        public Task<List<CryptoCurrencyDto>> GetAllCryptosAsync();
        public Task<CryptoCurrencyDto> GetCryptoAsync(int cryptoId);
        public Task<CryptoCurrencyDto> CreateCryptoAsnyc(CryptoCurrencyCreateDto CreateCrypto);
        public Task<bool> DeleteCryptoAsync(int cryptoId);
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
    }
}
