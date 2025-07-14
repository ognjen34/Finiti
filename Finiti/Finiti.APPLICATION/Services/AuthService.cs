using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Repositories;
using Finiti.DOMAIN.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.APPLICATION.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public Task<Author> Login(string username, string password)
        {
            Author author = _authorRepository.GetByUsernameAndPassword(username, password).Result;
            return Task.FromResult(author);
        }

        public Task<Author> Register(Author author)
        {
            Author author1 = _authorRepository.Add(author).Result;
            return Task.FromResult(author1);
        }
    }
}
