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
        }
    }
}
