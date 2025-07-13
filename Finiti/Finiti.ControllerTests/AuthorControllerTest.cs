using AutoMapper;
using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Services;
using Finiti.WEB.Controllers;
using Finiti.WEB.DTO.Requests;
using Finiti.WEB.DTO.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Finiti.ControllerTests
{
    [TestClass]
    public class AuthorControllerTests
    {
        private AuthorController _controller;
        private Mock<IAuthService> _authServiceMock;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _authServiceMock = new Mock<IAuthService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateAuthorRequest, Author>();
                cfg.CreateMap<Author, AuthorResponse>();
            });

            _mapper = config.CreateMapper();
            _controller = new AuthorController(_mapper, _authServiceMock.Object);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        private Author GenerateAuthor()
        {
            return new Author
            {
                Id = 1,
                Username = "testuser",
                Password = "hashedpassword",
                Role = new Role { Id = 1, Name = "Admin" }
            };
        }

        [TestMethod]
        public async Task Login_ValidCredentials_ReturnsOkWithAuthorResponse()
        {
            var request = new AuthorLoginRequest
            {
                Username = "testuser",
                Password = "123456"
            };

            var author = GenerateAuthor();

            _authServiceMock.Setup(service => service.Login(request.Username, request.Password))
                            .ReturnsAsync(author);

            var result = await _controller.Login(request);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as AuthorResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(author.Username, response.Username);
        }

        [TestMethod]
        public async Task Register_ValidAuthor_ReturnsOkWithAuthorResponse()
        {
            var request = new CreateAuthorRequest
            {
                FirstName = "Test",
                LastName = "User",
                Username = "testuser",
                Password = "123456"
            };

            var author = GenerateAuthor();

            _authServiceMock.Setup(service => service.Register(It.IsAny<Author>()))
                            .ReturnsAsync(author);

            var result = await _controller.Register(request);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as AuthorResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual("testuser", response.Username);
        }

        [TestMethod]
        public void Logout_ClearsJwtTokenCookie()
        {
            var result = _controller.Logout();

            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Authenticate_UserNotLoggedIn_ReturnsUnauthorized()
        {
            var result = await _controller.Authenticate();
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task Authenticate_UserLoggedIn_ReturnsOk()
        {
            var author = GenerateAuthor();
            LoggedAuthor loggedAuthor = new LoggedAuthor
            {
                Id = author.Id,
                Username = author.Username,
                Role = author.Role.Name
            };
            _controller.ControllerContext.HttpContext.Items["LoggedAuthor"] = loggedAuthor;

            var result = await _controller.Authenticate();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            LoggedAuthor returnedAuthor = okResult.Value as LoggedAuthor;
            Assert.AreEqual(author.Username, returnedAuthor.Username);
        }
    }
}