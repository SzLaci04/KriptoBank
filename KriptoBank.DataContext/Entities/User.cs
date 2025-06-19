using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KriptoBank.DataContext.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Wallet? Wallet { get; set; }
        public List<CryptoTransaction> CryptoTransactions { get; set; }=new List<CryptoTransaction>(){ };
    }
}
