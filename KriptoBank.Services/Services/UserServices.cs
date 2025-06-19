using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KriptoBank.DataContext.Context;
using KriptoBank.DataContext.Dtos;
using KriptoBank.DataContext.Entities;

namespace KriptoBank.Services.Services
{
    public interface IUserServices
    {
        Task<UserDataDto> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
        Task<UserDataDto> GetUserByIdAsync(int userId);
        Task<UserDataDto> UpdateUserPasswordAsync(int userId, UserUpdatePasswordDto userUpdate);
        Task<bool>DeleteUserAsync(int userId);
    }
    public class UserServices : IUserServices
    {
        private AppDbContext _appDbContext;
        private IMapper _mapper;
        public UserServices(AppDbContext context,IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }
        public Task<bool> DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDataDto> GetUserByIdAsync(int userId)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            return _mapper.Map<UserDataDto>(user);
        }

        public async Task<UserDataDto> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            var user=_mapper.Map<User>(userRegistrationDto);
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<UserDataDto>(user);
        }

        public async Task<UserDataDto> UpdateUserPasswordAsync(int userId, UserUpdatePasswordDto userUpdate)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            _mapper.Map(userUpdate,user);
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<UserDataDto>(user);
        }
    }
}