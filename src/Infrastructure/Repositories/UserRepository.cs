using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Core.Application.DTOs;
using Core.Domain.Abstracts;
using Core.Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> AddUser(AddUserDTO addUser)
        {
            var user = _mapper.Map<User>(addUser);

            _context.Users.Add(user);
            Console.WriteLine("Added async");

            await _context.SaveChangesAsync();
            Console.WriteLine("Saved async");
            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}