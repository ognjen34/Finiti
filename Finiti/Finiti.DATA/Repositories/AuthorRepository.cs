using AutoMapper;
using Finiti.DATA.Model;
using Finiti.DOMAIN.Exceptions;
using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DATA.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IMapper _mapper;
        private readonly DbSet<AuthorEntity> _authors;
        private readonly DatabaseContext _context;

        public AuthorRepository(DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _authors = context.Authors;
            _context = context;
        }
        public Task<Author> Add(Author author)
        {
            AuthorEntity existingAuthor = _authors.FirstOrDefault(a => a.Username == author.Username);
            if (existingAuthor != null)
            {
                throw new AuthorAlreadyExistsException("Author with this username already exists.");
            }
            AuthorEntity authorEntity = _mapper.Map<AuthorEntity>(author);
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(authorEntity.Password);
            authorEntity.Password = passwordHash;
            authorEntity.RoleId = 0;
            authorEntity.Role = null;

            _authors.Add(authorEntity);
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<Author>(authorEntity));
        }

        public Task<Author> GetById(int id)
        {

            AuthorEntity authorEntity = _authors.FirstOrDefault(a => a.Id == id);
            if (authorEntity == null)
            {
                throw new AuthorNotFoundException("Author not found with the provided ID.");
            }
            return Task.FromResult(_mapper.Map<Author>(authorEntity));
        }

        public Task<Author> GetByUsername(string username)
        {

            AuthorEntity authorEntity = _authors.FirstOrDefault(a => a.Username == username);
            if (authorEntity == null)
            {
                throw new AuthorNotFoundException("Author not found with the provided username.");
            }
            return Task.FromResult(_mapper.Map<Author>(authorEntity));
        }

        public Task<Author> GetByUsernameAndPassword(string username, string password)
        {
            AuthorEntity authorEntity = _authors.FirstOrDefault(a => a.Username == username);
            if (authorEntity == null)
            {
                throw new AuthorNotFoundException("Author not found with the provided username.");
            }
            if (!BCrypt.Net.BCrypt.Verify(password, authorEntity.Password))
            {
                throw new InvalidPasswordException("Invalid password for the provided username.");
            }
            return Task.FromResult(_mapper.Map<Author>(authorEntity));

        }
    }
}
