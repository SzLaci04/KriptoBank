using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.DataContext.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Balance { get; set; }

        public User User { get; set; } 
        public List<UserCryptoCurrency> UserCurrencies { get; set; }=new List<UserCryptoCurrency>();
    }
}
