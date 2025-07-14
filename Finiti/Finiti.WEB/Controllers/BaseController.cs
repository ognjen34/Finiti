using AutoMapper;
using Finiti.DOMAIN.Model;
using Microsoft.AspNetCore.Mvc;

namespace Finiti.WEB.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected LoggedAuthor LoggedAuthor => (LoggedAuthor)HttpContext.Items["LoggedAuthor"];

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
