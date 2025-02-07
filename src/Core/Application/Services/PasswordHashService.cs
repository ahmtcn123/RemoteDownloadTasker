using System.Security.Cryptography;
using Core.Domain.Abstracts;

namespace Core.Application.Services;

public class PasswordHashService : IPasswordHashService
{
    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        string[] parts = hashedPassword.Split(':');
        byte[] salt = Convert.FromBase64String(parts[0]);
        string originalHash = parts[1];

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        string newHash = Convert.ToBase64String(pbkdf2.GetBytes(32));

        return newHash == originalHash;
    }
}