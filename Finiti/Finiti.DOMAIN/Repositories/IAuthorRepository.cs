using Finiti.DOMAIN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> GetById(int id);
        Task<Author> GetByUsername(string username);
        Task<Author> Add(Author author);
        Task<Author> GetByUsernameAndPassword(string username,string password);
    }
}
