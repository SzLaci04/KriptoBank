using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.DataContext.Entities
{
    public class CryptoCurrency
    {
        public int Id { get; set; }
        public string Acronym { get; set; }
        public string Name { get; set; }
        public float CurrentPrice { get; set; }
        public int TotalAmount { get; set; }
        public float AvgPrice { get; set; }
        public bool IsDeleted {  get; set; }=false;

        public List<CryptoHistory> CurrencyHistory { get; set; }
    }
}
