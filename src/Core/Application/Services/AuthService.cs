using AutoMapper;

using Core.Application.Abstracts;
using Core.Application.DTOs;
using Core.Domain.Abstracts;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public Task<(bool Success, string Message, string Token)> Login(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<AuthResponse>> Register(RegisterRequest user)
        {
            var isEmailTaken = await _userRepository.GetUserByEmail(user.Email);
            Console.WriteLine($"Is email taken $isEmailTaken");
            if (isEmailTaken != null)
            {
                return new GenericResponse<AuthResponse>
                {
                    Success = false,
                    Message = "Email is already taken",
                    Data = new AuthResponse
                    {
                        AccessToken = "",
                        RefreshToken = ""
                    }
                };
            }

            //Log user data
            Console.WriteLine($"User: {user}");

            var newUserData = _mapper.Map<AddUserDTO>(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            newUserData.RefreshToken = refreshToken;


            Console.WriteLine($"newUserData: {newUserData.Email}, {newUserData.FirstName}, {newUserData.LastName}, {newUserData.Password}, {newUserData.RefreshToken}");
            var newUser = await _userRepository.AddUser(newUserData);
            var accessToken = _tokenService.GenerateAccessToken(newUser);

            Console.WriteLine("User created refresh token");

            //await _userRepository.UpdateUser(newUser);

            return new GenericResponse<AuthResponse>
            {
                Success = true,
                Message = "User registered successfully",
                Data = new AuthResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                }
            };
        }
    }
}