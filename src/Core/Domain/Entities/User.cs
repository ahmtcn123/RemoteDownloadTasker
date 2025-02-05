
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Abstracts;

namespace Core.Domain.Entities
{

    public enum UserRole
    {
        Admin,
        User
    }

    public class User : GenericFields
    {
        [Column("first_name")]
        [MaxLength(255)]
        public required string FirstName { get; set; }

        [Column("last_name")]
        [MaxLength(255)]
        public required string LastName { get; set; }

        [Column("email")]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Column("password")]
        [MaxLength(255)]
        public required string Password { get; set; }

        [Column("role")]
        public required UserRole Role { get; set; }

        [Column("refresh_token")]
        [MaxLength(255)]
        public required string RefreshToken { get; set; }
    }
}
