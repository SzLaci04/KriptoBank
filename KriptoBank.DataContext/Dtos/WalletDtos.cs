using KriptoBank.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.DataContext.Dtos
{
    public class WalletCurrentStateDto
    { 
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Balance { get; set; }
        public List<UserCryptoCurrencyDto> UserCryptoCurrencies { get; set; }=new List<UserCryptoCurrencyDto>();
    }
    public class WalletUpdateDto
    {
        [Required]
        public float Balance { get; set; }
    }
}
