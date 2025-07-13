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
                    //LoggedUser loggedUser = new LoggedUser();
                    //loggedUser.UserId = new Guid(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    //loggedUser.Name = identity.FindFirst(ClaimTypes.Name)?.Value;
                    //loggedUser.Email = identity.FindFirst(ClaimTypes.Email)?.Value;
                    //loggedUser.Role = Enum.Parse<Role>(identity.FindFirst(ClaimTypes.Role)?.Value);
                    //context.Items["loggedUser"] = loggedUser;


                }
                catch
                {
                }
            }

            await _next(context);
        }

    }
}
