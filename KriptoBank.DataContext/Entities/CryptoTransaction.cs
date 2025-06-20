using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.DataContext.Entities
{
    public enum TransactionType
    {
        buy,
        sell
    }
    public class CryptoTransaction
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? CryptoId { get; set; }
        public TransactionType Type { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public float TotalPrice { get; set; }
        public DateTime TimeOfTransaction { get; set; }=DateTime.UtcNow;

        public User User { get; set; }
    }
}
