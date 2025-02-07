namespace Core.Application.DTOs;

public class JwtSettings
{
    public required string Secret { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int ExpireInMinutes { get; set; }
}

public class ConnectionStrings
{
    public required string DefaultConnection { get; set; }
}

public class AppSettings
{
    public required JwtSettings Jwt { get; set; }
    public required ConnectionStrings ConnectionStrings { get; set; }
    public required int RefreshTokenExpiryInDays { get; set; }
}