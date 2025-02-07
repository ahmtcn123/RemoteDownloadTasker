using Core.Application.DTOs;
using Core.Domain.Entities;

namespace Core.Domain.Abstracts
{
    public interface IAuthService
    {
        Task<GenericResponse<AuthSerivceResponse>> Register(RegisterRequest user);
        Task<GenericResponse<AuthSerivceResponse>> Login(LoginRequest user);
        
        Task<GenericResponse<MeResponse>> Me(int userId);
        
        Task<GenericResponse<AuthSerivceResponse>> RefreshToken(string token);
    }
}