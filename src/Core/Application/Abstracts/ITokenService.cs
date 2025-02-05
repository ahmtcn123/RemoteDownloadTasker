
using System.Security.Claims;
using Core.Domain.Entities;

namespace Core.Application.Abstracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}