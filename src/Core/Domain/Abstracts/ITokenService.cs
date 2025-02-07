
using System.Security.Claims;
using Core.Domain.Entities;

namespace Core.Domain.Abstracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}