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
    public class ForbiddenWordRepositoryTests
    {
        private DatabaseContext _context;
        private IForbiddenWordRepository _repository;
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
                cfg.CreateMap<ForbiddenWord, ForbiddenWordEntity>().ReverseMap();
            });

            _mapper = config.CreateMapper();
            _repository = new ForbiddenWordRepository(_context, _mapper);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private ForbiddenWord GenerateWord(string word = "banned")
        {
            return new ForbiddenWord
            {
                Word = word
            };
        }

        [TestMethod]
        public async Task Add_ValidWord_Success()
        {
            var word = GenerateWord();

            var result = await _repository.Add(word);

            Assert.IsNotNull(result);
            Assert.AreEqual(word.Word, result.Word);
        }

        [TestMethod]
        public async Task Add_DuplicateWord_ThrowsException()
        {
            var word = GenerateWord();
            await _repository.Add(word);

            Assert.ThrowsException<ForbiddenWordAlreadyExsistsException>(() =>
                _repository.Add(word).GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task GetById_ExistingWord_Success()
        {
            var word = await _repository.Add(GenerateWord("prohibited"));

            var fetched = await _repository.GetById(word.Id);

            Assert.AreEqual(word.Word, fetched.Word);
        }

        [TestMethod]
        public void GetById_NonExistingWord_ThrowsException()
        {
            Assert.ThrowsException<ResourceNotFoundException>(() =>
                _repository.GetById(999).GetAwaiter().GetResult());
        }

        [TestMethod]
        public async Task GetAll_ReturnsAllWords()
        {
            await _repository.Add(GenerateWord("a"));
            await _repository.Add(GenerateWord("b"));
            await _repository.Add(GenerateWord("c"));

            var all = await _repository.GetAll();

            Assert.AreEqual(3, all.Count);
        }

        [TestMethod]
        public async Task DeleteByWord_ExistingWord_Success()
        {
            var word = await _repository.Add(GenerateWord("delete-me"));

            var result = await _repository.DeleteByWord("delete-me");

            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.ForbiddenWords.Count());
        }

        [TestMethod]
        public void DeleteByWord_NonExistingWord_ThrowsException()
        {
            Assert.ThrowsException<ResourceNotFoundException>(() =>
                _repository.DeleteByWord("nonexistent").GetAwaiter().GetResult());
        }
    }
}
