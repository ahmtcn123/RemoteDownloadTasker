using System.Security.Claims;
using AutoMapper;
using Core.Domain.Abstracts;
using Core.Application.DTOs;
using Core.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(GenericResponse<AuthResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericNullResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenericResponse<AuthResponse>>> Register(RegisterRequest user)
        {
            var result = await authService.Register(user);

            if (!result.Success)
                return new GenericResponse<AuthResponse>
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = null
                };

            var authResponse = mapper.Map<AuthResponse>(result.Data);

            var refreshTokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            // Set cookie
            Response.Cookies.Append("refreshToken", result.Data!.RefreshToken, refreshTokenOptions);

            return new GenericResponse<AuthResponse>
            {
                Success = result.Success,
                Message = result.Message,
                Data = authResponse
            };
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(GenericResponse<AuthResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericNullResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericNullResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GenericResponse<AuthResponse>>> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await authService.Login(loginRequest);

            if (!result.Success)
                return new GenericResponse<AuthResponse>
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = null
                };

            var authResponse = mapper.Map<AuthResponse>(result.Data);
            var refreshTokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            // Set cookie
            Response.Cookies.Append("refreshToken", result.Data!.RefreshToken, refreshTokenOptions);

            return new GenericResponse<AuthResponse>
            {
                Success = result.Success,
                Message = result.Message,
                Data = authResponse
            };
        }

        [HttpPost("refreshToken")]
        [ProducesResponseType(typeof(GenericResponse<AuthResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericNullResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericNullResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GenericResponse<AuthResponse>>> RefreshToken()
        {
            //Read token from Response cookies
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }
            
            var result = await authService.RefreshToken(refreshToken);
            
            if (!result.Success)
                return new GenericResponse<AuthResponse>
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = null
                };
            
            var authResponse = mapper.Map<AuthResponse>(result.Data);
            var refreshTokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            // Set cookie
            Response.Cookies.Append("refreshToken", result.Data!.RefreshToken, refreshTokenOptions);
            
            return new GenericResponse<AuthResponse>
            {
                Success = result.Success,
                Message = result.Message,
                Data = authResponse
            };
        }

        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(typeof(GenericResponse<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericNullResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GenericResponse<User>>> Me(IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext?.User;
            Console.WriteLine($"UserID: {userId.FindFirst(ClaimTypes.NameIdentifier)?.Value}");

            foreach (var claim in userId.Claims)
            {
                Console.WriteLine($"Claim: {claim.Value} {claim}");
            }

            return new GenericResponse<User>
            {
                Success = true,
                Message = "OK",
                Data = new User
                {
                    FirstName = "Ahmet",
                    LastName = "Last",
                    Email = "ahmet@mail.com",
                    Password = "1234",
                    Role = UserRole.Admin,
                    RefreshToken = "1234",
                    RefreshTokenExpiry = DateTime.Now
                }
            };
        }
    }
}