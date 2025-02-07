using Core.Application.DTOs;
using Core.Application.Exceptions;
using Npgsql;

namespace Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Request.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                InternalServerErrorException => StatusCodes.Status500InternalServerError,
                ConflictException => StatusCodes.Status409Conflict,
                ForbiddenException => StatusCodes.Status403Forbidden,
                NpgsqlException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            //If running dev
            
            var response = new GenericNullResponse
            {
                Success = false,
                Message = ex.Message,
            };
            
            await context.Response.WriteAsJsonAsync(response);
            //_logger.LogError(ex, "Unhandled exception occurred");
            //await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new { message = "Internal Server Error", details = exception.Message };
        return context.Response.WriteAsJsonAsync(response);
    }
}