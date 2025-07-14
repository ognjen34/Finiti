using Finiti.DOMAIN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Services
{
    public interface IAuthService
    {
        public Task<Author> Login(string username, string password);
        public Task<Author> Register(Author author);
    }
}
