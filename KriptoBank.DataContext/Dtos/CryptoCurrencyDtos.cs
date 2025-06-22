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
        public int WalletId { get; set; }
        public int CryptoId { get; set; }
        public float PriceAtBuy { get; set; }
        public float Amount { get; set; }
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
        [Required(ErrorMessage ="Rövidítés megadása kötelező")]
        [MaxLength(9,ErrorMessage ="Maximum 9 karakter lehet")]
        [MinLength(3, ErrorMessage = "Minimum 3 karakter kell")]
        public string Acronym { get; set; }
        [Required(ErrorMessage ="Név megadása kötelező")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Kezdeti érték megadása kötelező")]
        [Range(1,10000,ErrorMessage ="1 és 10000 között kell hogy legyen")]
        public float StartPrice { get; set; }
        [Required(ErrorMessage = "Kezdeti darabszám megadása kötelező")]
        [Range(1, 10000, ErrorMessage = "1 és 10000 között kell hogy legyen")]
        public int StartAmount { get; set; }
    }

    public class CryptoPriceUpdateDto
    {
        [Required(ErrorMessage ="Azonosító megadása kötelező")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Kezdeti érték megadása kötelező")]
        [Range(1, 10000, ErrorMessage = "1 és 10000 között kell hogy legyen")]
        public float NewPrice { get; set; }
    }
    public class CryptoHistoryDto
    {
        public int Id { get; set; }
        public int CryptoId { get; set; }
        public float CurrentPrice { get; set; }
        public float OldPrice { get; set; }
        public DateTime TimeOfChange { get; set; }
    }
}
