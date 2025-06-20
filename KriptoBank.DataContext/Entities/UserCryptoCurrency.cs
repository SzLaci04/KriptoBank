using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.DataContext.Entities
{
    public class UserCryptoCurrency
    {
        public int Id { get; set; }
        public int? WalletId { get; set; }
        public int? CryptoId { get; set; }
        public float PriceAtBuy { get; set; }
        public int Amount {  get; set; }
        public Wallet Wallet { get; set; }
    }
}
