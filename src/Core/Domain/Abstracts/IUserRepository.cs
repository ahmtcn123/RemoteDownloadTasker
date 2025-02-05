
using Core.Application.DTOs;
using Core.Domain.Entities;

namespace Core.Domain.Abstracts
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);
        Task<User> AddUser(AddUserDTO user);
        Task<User> UpdateUser(User user);
    }
}

