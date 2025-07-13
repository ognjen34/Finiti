using Finiti.DOMAIN.Exceptions;
using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Repositories;
using Finiti.DOMAIN.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.APPLICATION.Services
{
    public class ForbiddenWordService : IForbiddenWordService
    {
        private readonly IForbiddenWordRepository _forbiddenWordRepository;
        public ForbiddenWordService(IForbiddenWordRepository forbiddenWordRepository)
        {
            _forbiddenWordRepository = forbiddenWordRepository;
        }
        public Task<ForbiddenWord> Add(ForbiddenWord forbiddenWord)
        {
            ForbiddenWord word = _forbiddenWordRepository.Add(forbiddenWord).Result;
            return Task.FromResult(word);

        }

        public Task<bool> DeleteByWord(string word)
        {
            return _forbiddenWordRepository.DeleteByWord(word);


        }
    }
}
