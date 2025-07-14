using Finiti.APPLICATION.Services;
using Finiti.DOMAIN.Exceptions;
using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Repositories;
using Finiti.DOMAIN.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finiti.ServiceTests
{
    [TestClass]
    public class GlossaryTermServiceTests
    {
        private Mock<IGlossaryTermRepository> _glossaryTermRepositoryMock;
        private Mock<ITermValidationService> _termValidationServiceMock;
        private GlossaryTermService _glossaryTermService;

        [TestInitialize]
        public void Setup()
        {
            _glossaryTermRepositoryMock = new Mock<IGlossaryTermRepository>();
            _termValidationServiceMock = new Mock<ITermValidationService>();
            _glossaryTermService = new GlossaryTermService(
                _glossaryTermRepositoryMock.Object,
                _termValidationServiceMock.Object
            );
        }

        private GlossaryTerm GenerateTerm(string term = "Test", GlossaryTermStatus status = GlossaryTermStatus.DRAFT)
        {
            return new GlossaryTerm
            {
                Id = 1,
                Term = term,
                Definition = "Test Test",
                CreatedAt = DateTime.Now,
                Status = status,
                Author = new Author { Id = 1, FirstName = "Test", LastName = "Author" },
                IsDeleted = false
            };
        }

        [TestMethod]
        public async Task Add_ValidGlossaryTerm_Success()
        {
            var term = GenerateTerm();

            _glossaryTermRepositoryMock.Setup(repo => repo.Add(term)).ReturnsAsync(term);

            var result = await _glossaryTermService.Add(term);

            Assert.IsNotNull(result);
            Assert.AreEqual(term.Term, result.Term);
            _glossaryTermRepositoryMock.Verify(repo => repo.Add(term), Times.Once);
        }

        [TestMethod]
        public async Task Archive_PublishedTerm_Success()
        {
            var term = GenerateTerm(status: GlossaryTermStatus.PUBLISHED);

            _glossaryTermRepositoryMock.Setup(repo => repo.GetById(term.Id)).ReturnsAsync(term);
            _glossaryTermRepositoryMock.Setup(repo => repo.Archive(term.Id)).ReturnsAsync(term);

            var result = await _glossaryTermService.Archive(term.Id);

            Assert.IsNotNull(result);
            _glossaryTermRepositoryMock.Verify(repo => repo.Archive(term.Id), Times.Once);
        }

        [TestMethod]
        public async Task Archive_TermNotPublished_ThrowsException()
        {
            var term = GenerateTerm(status: GlossaryTermStatus.DRAFT);

            _glossaryTermRepositoryMock.Setup(repo => repo.GetById(term.Id)).ReturnsAsync(term);

            await Assert.ThrowsExceptionAsync<TermNotPublishedException>(() =>
                _glossaryTermService.Archive(term.Id));
        }

        [TestMethod]
        public async Task Delete_DraftTerm_Success()
        {
            var term = GenerateTerm(status: GlossaryTermStatus.DRAFT);

            _glossaryTermRepositoryMock.Setup(repo => repo.GetById(term.Id)).ReturnsAsync(term);
            _glossaryTermRepositoryMock.Setup(repo => repo.Delete(term.Id)).ReturnsAsync(term);

            var result = await _glossaryTermService.Delete(term.Id);

            Assert.IsNotNull(result);
            _glossaryTermRepositoryMock.Verify(repo => repo.Delete(term.Id), Times.Once);
        }

        [TestMethod]
        public async Task Delete_NonDraftTerm_ThrowsException()
        {
            var term = GenerateTerm(status: GlossaryTermStatus.PUBLISHED);

            _glossaryTermRepositoryMock.Setup(repo => repo.GetById(term.Id)).ReturnsAsync(term);

            await Assert.ThrowsExceptionAsync<TermNotInDraftException>(() =>
                _glossaryTermService.Delete(term.Id));
        }

        [TestMethod]
        public async Task GetById_ValidId_ReturnsGlossaryTerm()
        {
            var term = GenerateTerm();

            _glossaryTermRepositoryMock.Setup(repo => repo.GetById(term.Id)).ReturnsAsync(term);

            var result = await _glossaryTermService.GetById(term.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(term.Id, result.Id);
        }

        [TestMethod]
        public async Task GetByName_ValidName_ReturnsGlossaryTerm()
        {
            var term = GenerateTerm("Testing Test");

            _glossaryTermRepositoryMock.Setup(repo => repo.GetByName(term.Term)).ReturnsAsync(term);

            var result = await _glossaryTermService.GetByName(term.Term);

            Assert.IsNotNull(result);
            Assert.AreEqual(term.Term, result.Term);
        }

        [TestMethod]
        public async Task Publish_ValidTerm_PassesValidation_PublishesSuccessfully()
        {
            var term = GenerateTerm("Cloud", GlossaryTermStatus.DRAFT);

            _glossaryTermRepositoryMock.Setup(repo => repo.GetById(term.Id)).ReturnsAsync(term);
            _termValidationServiceMock.Setup(v => v.IsValidTerm(term)).ReturnsAsync(true);
            _glossaryTermRepositoryMock.Setup(repo => repo.Publish(term.Id)).ReturnsAsync(term);

            var result = await _glossaryTermService.Publish(term.Id);

            Assert.IsNotNull(result);
            _glossaryTermRepositoryMock.Verify(repo => repo.Publish(term.Id), Times.Once);
        }

        [TestMethod]
        public async Task Publish_ValidationFails_ThrowsException()
        {
            var term = GenerateTerm("Cloud", GlossaryTermStatus.DRAFT);

            _glossaryTermRepositoryMock.Setup(repo => repo.GetById(term.Id)).ReturnsAsync(term);
            _termValidationServiceMock.Setup(v => v.IsValidTerm(term)).ReturnsAsync(false);

            await Assert.ThrowsExceptionAsync<Exception>(() =>
                _glossaryTermService.Publish(term.Id));
        }

        [TestMethod]
        public async Task Search_WithPagination_ReturnsPaginatedResult()
        {
            var paginationFilter = new PaginationFilter { PageNumber = 1, PageSize = 10 };
            var terms = new List<GlossaryTerm> { GenerateTerm(), GenerateTerm("DevOps") };
            var paginatedResult = new PaginationReturnObject<GlossaryTerm>(terms, 1, 10, 2);

            _glossaryTermRepositoryMock.Setup(repo => repo.Search(paginationFilter)).ReturnsAsync(paginatedResult);

            var result = await _glossaryTermService.Search(paginationFilter);

            Assert.AreEqual(2, result.TotalItems);
            Assert.AreEqual(1, result.Page);
        }
    }
}
