using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.DataContext.Dtos
{
    public class UserCryptoCurrencyDto
    {
        public int Id { get; set; }
        public string Acronym { get; set; }
        public string Name { get; set; }
        public float TotalAmount { get; set; }
        public float AvgPrice { get; set; }
        public float CurrentPrice { get; set; }
        public float BuyPrice { get; set; }
    }
    public class CryptoCurrencyDto
    {
        public int Id { get; set; }
        public string Acronym { get; set; }
        public string Name { get; set; }
        public float TotalAmount { get; set; }
        public float AvgPrice { get; set; }
        public float CurrentPrice { get; set; }
    }
    public class CryptoCurrencyCreateDto
    {
        [Required]
        [MaxLength(9)]
        public string Acronym { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float StartPrice { get; set; }
        [Required]
        public int StartAmount { get; set; }
    }

    public class CryptoPriceUpdateDto
    {
        public int Id { get; set; }
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
