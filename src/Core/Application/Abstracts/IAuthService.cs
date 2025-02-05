using Core.Application.DTOs;
using Core.Domain.Entities;

namespace Core.Application.Abstracts
{
    public interface IAuthService
    {
        Task<GenericResponse<AuthResponse>> Register(RegisterRequest user);
        Task<(bool Success, string Message, string Token)> Login(User user);
    }
}