using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using KriptoBank.DataContext.Context;
using KriptoBank.DataContext.Dtos;
using KriptoBank.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace KriptoBank.Services.Services
{
    public class CryptoBackgroundService : BackgroundService
    {
        private readonly IBackGroundServiceProvider _backgroundServiceProvider;
        
        public CryptoBackgroundService(IBackGroundServiceProvider backgroundServiceProvider)
        {
            _backgroundServiceProvider = backgroundServiceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _backgroundServiceProvider.AutoPriceUpdate();
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }
}
