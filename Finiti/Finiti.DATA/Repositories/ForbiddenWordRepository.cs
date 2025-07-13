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
    public class ForbiddenWordRepository : IForbiddenWordRepository

    {

        private readonly IMapper _mapper;
        private readonly DbSet<ForbiddenWordEntity> _forbiddenWords;
        private readonly DatabaseContext _context;

        public ForbiddenWordRepository(DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _forbiddenWords = context.ForbiddenWords;
            _context = context;
        }
        public Task<ForbiddenWord> Add(ForbiddenWord forbiddenWord)
        {
            ForbiddenWordEntity existingForbiddenWord = _forbiddenWords.FirstOrDefault(fw => fw.Word == forbiddenWord.Word);
            if (existingForbiddenWord != null)
            {
                throw new ForbiddenWordAlreadyExsistsException("Forbidden word already exists.");
            }
            ForbiddenWordEntity forbiddenWordEntity = _mapper.Map<ForbiddenWordEntity>(forbiddenWord);
            _forbiddenWords.Add(forbiddenWordEntity);
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<ForbiddenWord>(forbiddenWordEntity));


        }

        public Task<bool> DeleteByWord(string word)
        {
            ForbiddenWordEntity forbiddenWordEntity = _forbiddenWords.FirstOrDefault(fw => fw.Word == word);
            if (forbiddenWordEntity == null)
            {
                throw new ResourceNotFoundException("Forbidden word not found with the provided word.");
            }
            _forbiddenWords.Remove(forbiddenWordEntity);
            _context.SaveChanges();
            return Task.FromResult(true);

        }

        public Task<List<ForbiddenWord>> GetAll()
        {
            List<ForbiddenWordEntity> forbiddenWordEntities = _forbiddenWords.ToList();
            return Task.FromResult(_mapper.Map<List<ForbiddenWord>>(forbiddenWordEntities));
            

        }

        public Task<ForbiddenWord> GetById(int id)
        {
            ForbiddenWordEntity forbiddenWordEntity = _forbiddenWords.FirstOrDefault(fw => fw.Id == id);
            if (forbiddenWordEntity == null)
            {
                throw new ResourceNotFoundException("Forbidden word not found with the provided ID.");
            }
            return Task.FromResult(_mapper.Map<ForbiddenWord>(forbiddenWordEntity));

        }
    }
}
