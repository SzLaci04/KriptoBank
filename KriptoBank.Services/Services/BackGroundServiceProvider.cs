using KriptoBank.DataContext.Context;
using KriptoBank.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.Services.Services
{
    public interface IBackGroundServiceProvider
    {
        // Define methods that the background service provider should implementű
        public Task AutoPriceUpdate();
    }
    public class BackGroundServiceProvider : IBackGroundServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public BackGroundServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task AutoPriceUpdate()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var cryptos = await appDbContext.CryptoCurrencies.Where(cc => !cc.IsDeleted).ToListAsync();
                foreach (var crypto in cryptos)
                {
                    Random r = new Random();
                    var oldPrice = crypto.CurrentPrice;
                    float newPrice = (float)Math.Round((1f+(float)(r.NextDouble() - 0.5) / 10)*oldPrice,2);
                    if (newPrice == 0)
                        newPrice = (float)Math.Round((1f + (float)(r.NextDouble()) / 10) * oldPrice, 2);
                    var newhistory = new CryptoHistory
                    {
                        CryptoId = crypto.Id,
                        OldPrice = crypto.CurrentPrice,
                        CurrentPrice = newPrice,
                        TimeOfChange = DateTime.Now,
                    };
                    crypto.CurrentPrice = newPrice;
                    await appDbContext.Histories.AddAsync(newhistory);
                    await appDbContext.SaveChangesAsync();
                    var histories = await appDbContext.Histories
                        .Where(h => h.CryptoId == crypto.Id)
                        .OrderByDescending(h => h.TimeOfChange)
                        .ToListAsync();
                    float avg = histories[0].OldPrice;
                    foreach (var history in histories)
                    {
                        avg += history.CurrentPrice;
                    }
                    avg /= histories.Count + 1;
                    crypto.AvgPrice = avg;
                    appDbContext.CryptoCurrencies.Update(crypto);
                    await appDbContext.SaveChangesAsync();
                }
            }
        }
    }

}
