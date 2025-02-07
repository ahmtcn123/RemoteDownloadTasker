using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Core.Application.DTOs;
using Core.Domain.Abstracts;
using Core.Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context, IMapper mapper) : IUserRepository
    {
        public async Task<User> AddUser(AddUserDto addUser)
        {
            var user = mapper.Map<User>(addUser);
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByRefreshToken(string refreshToken)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}