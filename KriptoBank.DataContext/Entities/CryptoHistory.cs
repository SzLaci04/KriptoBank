using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.DataContext.Entities
{
    public class CryptoHistory
    {
        public int Id { get; set; }
        public int? CryptoId { get; set; }
        public DateTime TimeOfChange { get; set; }=DateTime.UtcNow;
        public float OldPrice { get; set; }
        public float CurrentPrice { get; set; }
        public CryptoCurrency Currency { get; set; }
    }
}
