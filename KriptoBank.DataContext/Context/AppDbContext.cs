using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KriptoBank.DataContext.Entities;

namespace KriptoBank.DataContext.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {}
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<CryptoCurrency> CryptoCurrencies { get; set; }
        public DbSet<CryptoHistory> Histories { get; set; }
        public DbSet<CryptoTransaction> Transactions { get; set; }
        public DbSet<UserCryptoCurrency> userCryptoCurrencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(u => u.User)
                .HasForeignKey<Wallet>(w => w.UserId)
                .OnDelete(DeleteBehavior.SetNull); 

            modelBuilder.Entity<Wallet>()
                .HasMany(u => u.UserCurrencies)
                .WithOne(uc => uc.Wallet)
                .HasForeignKey(uc => uc.WalletId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CryptoCurrency>()
                .HasMany(c => c.CurrencyHistory)
                .WithOne(ch => ch.Currency)
                .HasForeignKey(ch => ch.CryptoId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CryptoTransactions)
                .WithOne(ct => ct.User)
                .HasForeignKey(ct => ct.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "user1", Password = "user11", Email = "user1@example.com", IsDeleted = false },
            new User { Id = 2, Username = "user2", Password = "user22", Email = "user2@example.com", IsDeleted = false },
            new User { Id = 3, Username = "user3", Password = "user33", Email = "user3@example.com", IsDeleted = false });

            modelBuilder.Entity<Wallet>().HasData(
            new Wallet { Id=1,UserId = 1, Balance = 4000.00F,IsDeleted=false },
            new Wallet { Id = 2, UserId = 2, Balance = 4000.00F, IsDeleted = false },
            new Wallet { Id = 3, UserId = 3, Balance = 4000.00F, IsDeleted = false });

            modelBuilder.Entity<CryptoCurrency>().HasData(
                new CryptoCurrency { Id = 1, Acronym = "BTC", Name = "Bitcoin", CurrentPrice = 98492.0f, TotalAmount = 8465, AvgPrice = 98492.0f, IsDeleted = false },
                new CryptoCurrency { Id = 2, Acronym = "ETH", Name = "Ethereum", CurrentPrice = 2139.21f, TotalAmount = 9362, AvgPrice = 2139.21f, IsDeleted = false },
                new CryptoCurrency { Id = 3, Acronym = "BNB", Name = "Binance Coin", CurrentPrice = 602.99f, TotalAmount = 4218, AvgPrice = 602.99f, IsDeleted = false },
                new CryptoCurrency { Id = 4, Acronym = "XRP", Name = "XRP", CurrentPrice = 1.94f, TotalAmount = 2941, AvgPrice = 1.94f, IsDeleted = false },
                new CryptoCurrency { Id = 5, Acronym = "USDT", Name = "Tether (Polygon)", CurrentPrice = 1.0f, TotalAmount = 6157, AvgPrice = 1.0f, IsDeleted = false },
                new CryptoCurrency { Id = 6, Acronym = "ADA", Name = "Cardano", CurrentPrice = 0.515608f, TotalAmount = 1550, AvgPrice = 0.515608f, IsDeleted = false },
                new CryptoCurrency { Id = 7, Acronym = "SOL", Name = "Solana", CurrentPrice = 126.87f, TotalAmount = 9822, AvgPrice = 126.87f, IsDeleted = false },
                new CryptoCurrency { Id = 8, Acronym = "DOGE", Name = "Dogecoin", CurrentPrice = 0.14508f, TotalAmount = 2774, AvgPrice = 0.14508f, IsDeleted = false },
                new CryptoCurrency { Id = 9, Acronym = "MATIC", Name = "Polygon", CurrentPrice = 0.165074f, TotalAmount = 6733, AvgPrice = 0.165074f, IsDeleted = false },
                new CryptoCurrency { Id = 10, Acronym = "DOT", Name = "Polkadot", CurrentPrice = 3.08f, TotalAmount = 7294, AvgPrice = 3.08f, IsDeleted = false },
                new CryptoCurrency { Id = 11, Acronym = "AVAX", Name = "Avalanche", CurrentPrice = 15.76f, TotalAmount = 1890, AvgPrice = 15.76f, IsDeleted = false },
                new CryptoCurrency { Id = 12, Acronym = "LINK", Name = "Chainlink", CurrentPrice = 11.05f, TotalAmount = 3933, AvgPrice = 11.05f, IsDeleted = false },
                new CryptoCurrency { Id = 13, Acronym = "LTC", Name = "Litecoin", CurrentPrice = 77.01f, TotalAmount = 5527, AvgPrice = 77.01f, IsDeleted = false },
                new CryptoCurrency { Id = 14, Acronym = "SHIB", Name = "Shiba Inu", CurrentPrice = 0.00001017f, TotalAmount = 1084, AvgPrice = 0.00001017f, IsDeleted = false },
                new CryptoCurrency { Id = 15, Acronym = "TRX", Name = "TRON", CurrentPrice = 0.26054f, TotalAmount = 7871, AvgPrice = 0.26054f, IsDeleted = false });
        }
    }
}
