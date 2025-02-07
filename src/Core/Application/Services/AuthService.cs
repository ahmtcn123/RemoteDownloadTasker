using AutoMapper;
using Core.Domain.Abstracts;
using Core.Application.DTOs;
using Core.Application.Exceptions;
using Core.Domain.Abstracts;
using Core.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Core.Application.Services
{
    public class AuthService(
        IUserRepository userRepository,
        IMapper mapper,
        ITokenService tokenService,
        IPasswordHashService passwordHashService,
        IConfiguration configuration
    )
        : IAuthService
    {
        public async Task<GenericResponse<AuthSerivceResponse>> Register(RegisterRequest user)
        {
            var isEmailTaken = await userRepository.GetUserByEmail(user.Email);
            if (isEmailTaken != null)
            {
                throw new BadRequestException("Email is already taken");
            }

            var newUserData = mapper.Map<AddUserDto>(user);
            var refreshToken = tokenService.GenerateRefreshToken();

            // Hash password
            newUserData.Password = passwordHashService.HashPassword(newUserData.Password);
            newUserData.RefreshToken = refreshToken;
            newUserData.RefreshTokenExpiry = DateTime.UtcNow.AddDays(2);

            var newUser = await userRepository.AddUser(newUserData);
            var accessToken = tokenService.GenerateAccessToken(newUser);

            return new GenericResponse<AuthSerivceResponse>
            {
                Success = true,
                Message = "User registered successfully",
                Data = new AuthSerivceResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = TimeSpan.FromMinutes(30)
                }
            };
        }

        public async Task<GenericResponse<AuthSerivceResponse>> Login(LoginRequest user)
        {
            var userInfo = await userRepository.GetUserByEmail(user.Email);

            if (userInfo == null)
            {
                throw new UnauthorizedException("Wrong email or password");
            }

            var passwordSuccess = passwordHashService.VerifyPassword(user.Password, userInfo.Password);
            if (!passwordSuccess)
            {
                throw new UnauthorizedException("Wrong email or password");
            }

            var accessToken = tokenService.GenerateAccessToken(userInfo);
            var refreshToken = tokenService.GenerateRefreshToken();

            userInfo.RefreshToken = refreshToken;
            userInfo.RefreshTokenExpiry = DateTime.UtcNow.AddDays(2);
            await userRepository.UpdateUser(userInfo);

            return new GenericResponse<AuthSerivceResponse>
            {
                Success = true,
                Message = "Welcome",
                Data = new AuthSerivceResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = TimeSpan.FromMinutes(30)
                }
            };
        }

        public Task<GenericResponse<MeResponse>> Me(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<AuthSerivceResponse>> RefreshToken(string token)
        {
            var foundUser = await userRepository.GetUserByRefreshToken(token);

            if (foundUser == null || foundUser.RefreshTokenExpiry < DateTime.UtcNow)
            {
            }

            //var principal = tokenService.GetPrincipalFromExpiredToken(token);
            Console.WriteLine($"Token: {token}");
            throw new NotImplementedException();
        }
    }
}