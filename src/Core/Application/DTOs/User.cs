using Core.Domain.Entities;

namespace Core.Application.DTOs
{
    public class AddUserDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required UserRole Role { get; set; }
        public required string RefreshToken { get; set; }
    }
}