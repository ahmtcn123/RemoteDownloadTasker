
namespace Core.Application.DTOs
{
    public class RegisterRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class AuthResponse
    {
        public required string AccessToken { get; set; }
        public required TimeSpan ExpiresIn { get; set; }
    }

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class MeResponse
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
    }

    public class AuthSerivceResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required TimeSpan ExpiresIn { get; set; }
    }
}