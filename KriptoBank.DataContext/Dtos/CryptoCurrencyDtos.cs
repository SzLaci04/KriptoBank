using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.DataContext.Dtos
{
    public class UserCryptoCurrencyDto
    {
        public int CryptoId { get; set; }
        public string Acronym { get; set; }
        public string CryptoName { get; set; }
        public float Amount { get; set; }
        public float AvgPrice { get; set; }
        public float CurrentPrice { get; set; }
        public float BuyPrice { get; set; }
    }
    public class CryptoCurrencyDto
    {
        public int CryptoId { get; set; }
        public string Acronym { get; set; }
        public string CryptoName { get; set; }
        public float Amount { get; set; }
        public float AvgPrice { get; set; }
        public float CurrentPrice { get; set; }
    }
    public class CryptoCurrencyCreateDto
    { 
        public int Id { get; set; }
        public string Acronym { get; set; }
        public string CryptoName { get; set; }
        public float StartPrice { get; set; }
        public int StartAmount { get; set; }
    }

    public class CryptoPriceUpdateDto
    {
        public int CryptoId { get; set; }
        public float NewPrice { get; set; }
    }
    public class CryptoHistoryDto
    {
        public int CryptoId { get; set; }
        public string Acronym { get; set; }
        public float CurrentPrice { get; set; }
        public DateTime Time { get; set; }
    }
}
