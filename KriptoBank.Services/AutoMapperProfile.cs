using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KriptoBank.DataContext.Entities;
using KriptoBank.DataContext.Dtos;

namespace KriptoBank.Services
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            //User mapping
            CreateMap<User,UserDataDto>().ReverseMap();
            CreateMap<UserRegistrationDto, User>();
            CreateMap<UserUpdatePasswordDto, User>();
            //Wallet mapping
            CreateMap<Wallet,WalletCurrentStateDto>().ReverseMap();
            //CryptoCurrency mapping
            CreateMap<CryptoCurrency, CryptoCurrencyDto>().ReverseMap();
            CreateMap<CryptoCurrencyCreateDto, CryptoCurrency>()
                .ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(src => src.StartPrice))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.StartAmount)); 
        }
    }
}
