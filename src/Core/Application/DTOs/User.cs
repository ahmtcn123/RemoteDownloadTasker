using Core.Domain.Entities;

namespace Core.Application.DTOs
{
    public class AddUserDto
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public required string Password { get; set; }
        public required UserRole Role { get; init; }
        public required string RefreshToken { get; set; }
        public required DateTime RefreshTokenExpiry { get; set; }
    }
}