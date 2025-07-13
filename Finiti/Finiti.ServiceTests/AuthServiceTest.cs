using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Finiti.APPLICATION.Services;
using Finiti.DOMAIN.Exceptions;
using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Repositories;
using Finiti.DOMAIN.Services;

namespace Finiti.ServiceTests
{
    [TestClass]
    public class AuthServiceTests
    {
        private Mock<IAuthorRepository> _authorRepositoryMock;
        private IAuthService _authService;

        [TestInitialize]
        public void TestInitialize()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _authService = new AuthService(_authorRepositoryMock.Object);
        }

        private Author GenerateAuthor(string username = "testuser")
        {
            return new Author
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Author",
                Username = username,
                Password = "test123"
            };
        }

        [TestMethod]
        public async Task Register_ValidAuthor_ReturnsAuthor()
        {
            var author = GenerateAuthor();

            _authorRepositoryMock.Setup(repo => repo.Add(author))
                .ReturnsAsync(author);

            var result = await _authService.Register(author);

            Assert.IsNotNull(result);
            Assert.AreEqual(author.Username, result.Username);
            _authorRepositoryMock.Verify(repo => repo.Add(author), Times.Once);
        }

        [TestMethod]
        public async Task Register_ExistingUsername_ThrowsAuthorAlreadyExistsException()
        {
            var author = GenerateAuthor();

            _authorRepositoryMock.Setup(repo => repo.Add(author))
                .Throws<AuthorAlreadyExistsException>();

            await Assert.ThrowsExceptionAsync<AuthorAlreadyExistsException>(() => _authService.Register(author));
        }

        [TestMethod]
        public async Task Login_ValidCredentials_ReturnsAuthor()
        {
            var username = "testuser";
            var password = "test123";
            var author = GenerateAuthor(username);

            _authorRepositoryMock.Setup(repo => repo.GetByUsernameAndPassword(username, password))
                .ReturnsAsync(author);

            var result = await _authService.Login(username, password);

            Assert.IsNotNull(result);
            Assert.AreEqual(username, result.Username);
            _authorRepositoryMock.Verify(repo => repo.GetByUsernameAndPassword(username, password), Times.Once);
        }

        [TestMethod]
        public async Task Login_InvalidCredentials_ThrowsResourceNotFoundException()
        {
            var username = "wronguser";
            var password = "wrongpass";

            _authorRepositoryMock.Setup(repo => repo.GetByUsernameAndPassword(username, password))
                .Throws<ResourceNotFoundException>();

            await Assert.ThrowsExceptionAsync<ResourceNotFoundException>(() => _authService.Login(username, password));
        }
    }
}
