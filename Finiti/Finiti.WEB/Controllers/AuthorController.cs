using AutoMapper;
using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Services;
using Finiti.WEB.DTO.Requests;
using Finiti.WEB.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Finiti.WEB.Controllers
{
    [Route("authors")]
    [ApiController]
    public class AuthorController : BaseController
    {
        private IAuthService _authService;
        private readonly string _jwt;
        public AuthorController(IMapper mapper,IAuthService authService,IConfiguration config) : base(mapper)
        {
            _authService = authService;
            _jwt = config["JWT"];
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthorLoginRequest request)
        {
            
            var author = await _authService.Login(request.Username, request.Password);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, author.Id.ToString()),
                new Claim(ClaimTypes.Name,author.Username),
                new Claim(ClaimTypes.Role, author.Role.Name)

            };


            //secret je ovde samo u developmentu

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt));
            var credentialsJWT = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims,
               expires: DateTime.Now.AddHours(24),
               signingCredentials: credentialsJWT
           );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            Response.Cookies.Append("jwtToken", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(24),
                SameSite = SameSiteMode.None



            });
            return Ok(_mapper.Map<AuthorResponse>(author));
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("jwtToken", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddYears(-1),
                SameSite = SameSiteMode.None
            });

            return Ok();
        }

        [HttpGet("authenticate")]
        public async Task<ActionResult> Authenticate()
        {
            if (LoggedAuthor == null)
            {
                return Unauthorized();

            }
            else
            {
                return Ok(LoggedAuthor);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateAuthorRequest request)
        {
            Author author = _mapper.Map<Author>(request);
            var registeredAuthor = await _authService.Register(author);
            return Ok(_mapper.Map<AuthorResponse>(registeredAuthor));
        }

    }
}
