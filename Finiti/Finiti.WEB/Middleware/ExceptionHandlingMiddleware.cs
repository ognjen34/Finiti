using Finiti.DOMAIN.Exceptions;
using System.Text.Json;

namespace Finiti.WEB.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            if (exception is AuthorNotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (exception is InvalidPasswordException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (exception is AuthorAlreadyExistsException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (exception is ForbiddenWordAlreadyExsistsException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (exception is ResourceNotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (exception is TermAlreadyExistsException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (exception is TermNotInDraftException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (exception is TermNotPublishedException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (exception is TermValidationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            context.Response.ContentType = "application/json";
            var errorResponse = new { error = $"Error : {exception.Message}" };
            var errorJson = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(errorJson);
        }
    }
}
