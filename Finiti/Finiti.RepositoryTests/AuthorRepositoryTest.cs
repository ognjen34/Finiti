using AutoMapper;
using Finiti.DATA;
using Finiti.DATA.Model;
using Finiti.DATA.Repositories;
using Finiti.DOMAIN.Exceptions;
using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Finiti.RepositoryTests
{
    [TestClass]
    public class AuthorRepositoryTest
    {
        private IAuthorRepository _authorRepository;
        private DatabaseContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DatabaseContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorEntity>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            _authorRepository = new AuthorRepository(_context, mapper);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Authors.RemoveRange(_context.Authors);
            _context.SaveChanges();
        }

        private Author GenerateAuthor(string username = "testuser")
        {
            return new Author
            {
                Username = username,
                Password = "password123",
                FirstName = "Test",
                LastName = "User"
            };
        }

        [TestMethod]
        public async Task Add_ValidAuthor_Success()
        {
            var author = GenerateAuthor();

            var result = await _authorRepository.Add(author);

            Assert.IsNotNull(result);
            Assert.AreEqual(author.Username, result.Username);
        }

        [TestMethod]
        public async Task Add_AuthorWithExistingUsername_ThrowsException()
        {
            var author = GenerateAuthor();
            await _authorRepository.Add(author);

            var duplicate = GenerateAuthor();

            Assert.ThrowsException<AuthorAlreadyExistsException>(() =>
                _authorRepository.Add(duplicate).GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task GetById_ExistingAuthor_ReturnsAuthor()
        {
            var author = GenerateAuthor();
            var added = await _authorRepository.Add(author);

            var fetched = await _authorRepository.GetById(added.Id);

            Assert.AreEqual(added.Username, fetched.Username);
        }

        [TestMethod]
        public void GetById_NonExistingAuthor_ThrowsException()
        {
            Assert.ThrowsException<AuthorNotFoundException>(() =>
                _authorRepository.GetById(999).GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task GetByUsername_ExistingAuthor_ReturnsAuthor()
        {
            var author = GenerateAuthor();
            await _authorRepository.Add(author);

            var fetched = await _authorRepository.GetByUsername(author.Username);

            Assert.AreEqual(author.Username, fetched.Username);
        }

        [TestMethod]
        public void GetByUsername_NonExistingAuthor_ThrowsException()
        {
            Assert.ThrowsException<AuthorNotFoundException>(() =>
                _authorRepository.GetByUsername("nonexistent").GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task GetByUsernameAndPassword_CorrectCredentials_ReturnsAuthor()
        {
            var author = GenerateAuthor();
            await _authorRepository.Add(author);

            var fetched = await _authorRepository.GetByUsernameAndPassword(author.Username, author.Password);

            Assert.AreEqual(author.FirstName, fetched.FirstName);
        }

        [TestMethod]
        public async Task GetByUsernameAndPassword_InvalidPassword_ThrowsException()
        {
            var author = GenerateAuthor();
            await _authorRepository.Add(author);

            Assert.ThrowsException<InvalidPasswordException>(() =>
                _authorRepository.GetByUsernameAndPassword(author.Username, "wrongpass").GetAwaiter().GetResult());
        }

        [TestMethod]
        public void GetByUsernameAndPassword_NonExistingUser_ThrowsException()
        {
            Assert.ThrowsException<AuthorNotFoundException>(() =>
                _authorRepository.GetByUsernameAndPassword("nonexistent", "any").GetAwaiter().GetResult());
        }
    }
}
