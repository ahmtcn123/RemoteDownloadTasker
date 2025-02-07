
using Core.Application.Mappings;
using Core.Domain.Abstracts;
using Core.Application.Services;
using Core.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHashService, PasswordHashService>();
        }

        public static void AddCoreMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(AuthProfile));
        }

        public static void AddCoreValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
            services.AddFluentValidationAutoValidation();
        }
    }
}