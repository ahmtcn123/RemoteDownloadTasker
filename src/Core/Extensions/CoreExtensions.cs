
using Core.Application.Abstracts;
using Core.Application.Mappings.Mappings;
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
        }

        public static void AddCoreMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
        }

        public static void AddCoreValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
            services.AddFluentValidationAutoValidation();
        }
    }
}