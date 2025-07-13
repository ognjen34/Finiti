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

            //if (exception is ResourceNotFoundException)
            //{
            //    context.Response.StatusCode = StatusCodes.Status404NotFound;
            //}
            //else if (exception is EmailAlreadyExistException)
            //{
            //    context.Response.StatusCode = StatusCodes.Status400BadRequest;
            //}
            //else
            //{
            //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //}
            //context.Response.ContentType = "application/json";
            return context.Response.WriteAsync($"Error : {exception.Message}");
        }
    }
}
