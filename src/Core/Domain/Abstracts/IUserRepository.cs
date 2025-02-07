
using Core.Application.DTOs;
using Core.Domain.Entities;

namespace Core.Domain.Abstracts
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);
        Task<User> AddUser(AddUserDto user);
        Task<User> UpdateUser(User user);
        Task<User?> GetUserByRefreshToken(string refreshToken);
    }
}

