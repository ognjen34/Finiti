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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finiti.RepositoryTests
{
    [TestClass]
    public class GlossaryTermRepositoryTests
    {
        private DatabaseContext _context;
        private IGlossaryTermRepository _repository;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DatabaseContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GlossaryTerm, GlossaryTermEntity>()
                    .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id))
                    .ForMember(dest => dest.Author, opt => opt.Ignore());

                cfg.CreateMap<GlossaryTermEntity, GlossaryTerm>();

                cfg.CreateMap<Author, AuthorEntity>().ReverseMap();
            });

            _mapper = config.CreateMapper();
            _repository = new GlossaryTermRepository(_context, _mapper);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private AuthorEntity SeedAuthor(string username = "testuser")
        {
            var author = new AuthorEntity
            {
                FirstName = "Test",
                LastName = "Author",
                Username = username,
                Password = "hashedPassword",
                RoleId = 0
            };
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }

        private GlossaryTerm GenerateTerm(int authorId, string term = "TestTerm",GlossaryTermStatus status = GlossaryTermStatus.DRAFT)
        {
            return new GlossaryTerm
            {
                Term = term,
                Definition = "Some definition",
                CreatedAt = DateTime.UtcNow,
                Status = GlossaryTermStatus.DRAFT,
                Author = new Author { Id = authorId },
                IsDeleted = false
            };
        }

        [TestMethod]
        public async Task Add_ValidTerm_Success()
        {
            var author = SeedAuthor();
            var term = GenerateTerm(author.Id);

            var result = await _repository.Add(term);

            Assert.IsNotNull(result);
            Assert.AreEqual(term.Term, result.Term);
        }

        [TestMethod]
        public async Task Add_DuplicateTerm_ThrowsException()
        {
            var author = SeedAuthor();
            var term = GenerateTerm(author.Id);

            term = await _repository.Add(term);
            await _repository.Publish(term.Id);



            Assert.ThrowsException<TermAlreadyExistsException>(() =>
                _repository.Add(term).GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task GetById_ExistingTerm_ReturnsCorrectTerm()
        {
            var author = SeedAuthor();
            var term = await _repository.Add(GenerateTerm(author.Id));

            var fetched = await _repository.GetById(term.Id);

            Assert.AreEqual(term.Term, fetched.Term);
        }

        [TestMethod]
        public void GetById_NonExisting_ThrowsException()
        {
            Assert.ThrowsException<ResourceNotFoundException>(() =>
                _repository.GetById(999).GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task GetByName_ExistingTerm_Success()
        {
            var author = SeedAuthor();
            var term = GenerateTerm(author.Id, "UniqueTerm");
            await _repository.Add(term);

            var fetched = await _repository.GetByName("UniqueTerm");

            Assert.AreEqual("UniqueTerm", fetched.Term);
        }

        [TestMethod]
        public void GetByName_NonExistingTerm_ThrowsException()
        {
            Assert.ThrowsException<ResourceNotFoundException>(() =>
                _repository.GetByName("nonexistent").GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task Delete_ExistingTerm_SetsIsDeletedTrue()
        {
            var author = SeedAuthor();
            var term = await _repository.Add(GenerateTerm(author.Id));

            await _repository.Delete(term.Id);

            var inDb = _context.GlossaryTerms.FirstOrDefault(t => t.Id == term.Id);
            Assert.IsTrue(inDb.IsDeleted);
        }

        [TestMethod]
        public void Delete_NonExisting_ThrowsException()
        {
            Assert.ThrowsException<ResourceNotFoundException>(() =>
                _repository.Delete(999).GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task Archive_ChangesStatusToArchived()
        {
            var author = SeedAuthor();
            var term = await _repository.Add(GenerateTerm(author.Id));

            var archived = await _repository.Archive(term.Id);

            Assert.AreEqual(GlossaryTermStatus.ARCHIVED, archived.Status);
        }

        [TestMethod]
        public async Task Publish_ChangesStatusToPublished()
        {
            var author = SeedAuthor();
            var term = await _repository.Add(GenerateTerm(author.Id));

            var published = await _repository.Publish(term.Id);

            Assert.AreEqual(GlossaryTermStatus.PUBLISHED, published.Status);
        }

        [TestMethod]
        public async Task Publish_AlreadyPublished_ThrowsException()
        {
            var author = SeedAuthor();
            var term = await _repository.Add(GenerateTerm(author.Id));
            await _repository.Publish(term.Id);

            Assert.ThrowsException<InvalidOperationException>(() =>
                _repository.Publish(term.Id).GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task Search_FiltersByTermAndAuthor()
        {
            var author = SeedAuthor();

            GlossaryTerm term1 = GenerateTerm(author.Id, "C#");
            GlossaryTerm term2 = GenerateTerm(author.Id, "JavaScript");
            GlossaryTerm term3 = GenerateTerm(author.Id, "Python");
            term1 = await _repository.Add(term1);
            term2 = await _repository.Add(term2);
            term3 = await _repository.Add(term3);
            await _repository.Publish(term1.Id);
            await _repository.Publish(term2.Id);
            await _repository.Publish(term3.Id);
            

            

            var filter = new PaginationFilter
            {
                PageNumber = 1,
                PageSize = 10,
                TermQuery = "C#",
                AuthorQuery = "Test"
            };

            var result = await _repository.Search(filter);

            Assert.AreEqual(1, result.Items.Count());
            Assert.AreEqual("C#", result.Items.First().Term);
        }
    }
}
