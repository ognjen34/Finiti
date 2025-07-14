using AutoMapper;
using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Services;
using Finiti.WEB.Controllers;
using Finiti.WEB.DTO.Requests;
using Finiti.WEB.DTO.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Finiti.DATA.Migrations;

namespace Finiti.ControllerTests
{
    [TestClass]
    public class GlossaryTermControllerTests
    {
        private Mock<IGlossaryTermService> _glossaryTermServiceMock;
        private Mock<IForbiddenWordService> _forbiddenWordServiceMock;
        private GlossaryTermController _controller;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _glossaryTermServiceMock = new Mock<IGlossaryTermService>();
            _forbiddenWordServiceMock = new Mock<IForbiddenWordService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateTermRequest, GlossaryTerm>();
                cfg.CreateMap<GlossaryTerm, TermResponse>();
                cfg.CreateMap<ForbiddenWordRequest, ForbiddenWord>();
                cfg.CreateMap<Author, AuthorResponse>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
            });

            _mapper = config.CreateMapper();

            _controller = new GlossaryTermController(_mapper, _glossaryTermServiceMock.Object, _forbiddenWordServiceMock.Object);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };
            httpContext.Items["LoggedAuthor"] = new LoggedAuthor { Id = 1 };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TestMethod]
        public async Task AddTerm_ValidRequest_ReturnsOkResult()
        {
            var request = new CreateTermRequest
            {
                Term = "Test",
                Definition = "Definition"
            };

            var glossaryTerm = new GlossaryTerm
            {
                Id = 1,
                Term = request.Term,
                Definition = request.Definition,
                Status = GlossaryTermStatus.DRAFT,
                Author = new Author { Id = 1 },
                CreatedAt = DateTime.UtcNow
            };

            _glossaryTermServiceMock.Setup(s => s.Add(It.IsAny<GlossaryTerm>())).ReturnsAsync(glossaryTerm);

            var result = await _controller.AddTerm(request);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var response = okResult.Value as TermResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual("Test", response.Term);
        }

        [TestMethod]
        public async Task PublishTerm_ValidId_ReturnsOk()
        {
            var glossaryTerm = new GlossaryTerm { Id = 1, Term = "Test" };
            _glossaryTermServiceMock.Setup(s => s.Publish(1)).ReturnsAsync(glossaryTerm);

            var result = await _controller.PublishTerm(1);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task AddForbiddenWord_Valid_ReturnsOk()
        {
            var request = new ForbiddenWordRequest { Word = "badword" };
            var forbiddenWord = new ForbiddenWord { Id = 1, Word = request.Word };

            _forbiddenWordServiceMock.Setup(s => s.Add(It.IsAny<ForbiddenWord>())).ReturnsAsync(forbiddenWord);

            var result = await _controller.AddForbiddenWord(request);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Forbidden word added successfully.", okResult.Value);
        }

        [TestMethod]
        public async Task DeleteForbiddenWord_Valid_ReturnsOk()
        {
            _forbiddenWordServiceMock.Setup(s => s.DeleteByWord("badword")).ReturnsAsync(true);

            var result = await _controller.DeleteForbiddenWord("badword");
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Forbidden word deleted successfully.", okResult.Value);
        }
    }
}
