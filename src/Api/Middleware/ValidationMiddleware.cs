using FluentValidation;

namespace Api.Middleware;

public class ValidationMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await next(context);
            return;
        }

        var parameters = endpoint.Metadata.GetMetadata<Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor>()?.Parameters;
        if (parameters == null)
        {
            await next(context);
            return;
        }

        foreach (var param in parameters)
        {
            var paramType = param.ParameterType;
            var validatorType = typeof(IValidator).MakeGenericType(paramType);
            var validator = serviceProvider.GetService(validatorType);
            
            if (validator == null) continue;

            var validateMethod = validatorType.GetMethod("ValidateAsync");
            var requestBody = await context.Request.ReadFromJsonAsync(paramType);

            if (requestBody == null) continue;

            var validationResult = await (Task<FluentValidation.Results.ValidationResult>)validateMethod.Invoke(validator, new[] { requestBody, default(CancellationToken) });

            if (validationResult.IsValid) continue;
            var response = new
            {
                Success = false,
                Message = validationResult.ToString()
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(response);
            return;
        }

        await next(context);
    }
}
