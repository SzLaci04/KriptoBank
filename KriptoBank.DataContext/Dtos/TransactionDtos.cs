﻿using KriptoBank.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.DataContext.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CryptoId { get; set; }
        public TransactionType Type { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public float TotalPrice { get; set; }
        public DateTime TimeOfTransaction { get; set; }
    }
    public class TransactionShortDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CryptoId { get; set; }
        public TransactionType Type { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public DateTime TimeOfTransaction { get; set; }
    }

    public class TransactionBuyDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CryptoId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
    public class TransactionSellDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CryptoId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
    public class ProfitSummaryDto
    {
        public int UserId { get; set; }
        public float TotalInvestment { get; set; }
        public float CurrentValue { get; set; }
        public float TotalChange { get; set; }
    }
    public class ProfitDetailDto
    {
        public ProfitSummaryDto Summary { get; set; }=new ProfitSummaryDto();
        public List<CryptoChangeDto> CryptoChanges { get; set; } = new List<CryptoChangeDto>();
    }
    public class CryptoChangeDto
    {
        public int CryptoId { get; set; }
        public string Acronym { get; set; }
        public string Name { get; set; }
        public float CryptoAmount { get; set; }
        public float AvgPrice { get; set; }
        public float CurrentPrice { get; set; }
        public float Change { get; set; }
    }

    public class PortfolioDto
    {
        public int WalletId { get; set; }
        public int UserId { get; set; }
        public List<UserCryptoCurrency> userCryptoCurrencies { get; set; } = new List<UserCryptoCurrency>();
        public float BaseBalance { get; set; }
        public float TotalBalance { get; set; }
    }
}
