using MiniShop.AuthService.Application.Common.Exceptions;
using MiniShop.AuthService.Domain.Exceptions;

namespace MiniShop.AuthService.API.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            context.Response.StatusCode = statusCode;

            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };

            await context.Response.WriteAsJsonAsync(response);

        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
        private static string GetTitle(Exception exception) =>
            exception switch
            {
                ValidationException => "Validation error",
                BadRequestException => "Bad Request",
                NotFoundException => "Not found",
                _ => "Server error"
            };
        private static IReadOnlyDictionary<string, string[]>? GetErrors(Exception exception) =>
            exception switch
            {
                ValidationException validation => validation.Errors,
                _ => null
            };
    }
}
