using Finiti.DOMAIN.Model;
using System.Security.Claims;

namespace Finiti.WEB.Middleware
{
    public class ClaimsMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity is ClaimsIdentity identity)
            {
                try
                {
                    LoggedAuthor loggedAuthor = new LoggedAuthor();
                    loggedAuthor.Id = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    loggedAuthor.Username = identity.FindFirst(ClaimTypes.Name)?.Value;
                    loggedAuthor.Role  = identity.FindFirst(ClaimTypes.Role)?.Value;
                    context.Items["LoggedAuthor"] = loggedAuthor;
                }
                catch
                {
                }
            }

            await _next(context);
        }

    }
}
