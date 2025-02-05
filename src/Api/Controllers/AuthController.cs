

using Core.Application.Abstracts;
using Core.Application.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<GenericResponse<AuthResponse>>> Register(RegisterRequest user, [FromServices] IValidator<RegisterRequest> validator)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(user);
                if (!validationResult.IsValid)
                {
                    Console.WriteLine(validationResult.ToString());
                    return BadRequest(new GenericResponse<AuthResponse>
                    {
                        Success = false,
                        Message = validationResult.ToString()
                    });
                }

                var result = await _authService.Register(user);
                if (result.Success)
                {
                    return result;
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(new GenericResponse<AuthResponse>
                {
                    Success = false,
                    Message = e.Message
                });
            }
        }
    }
}
