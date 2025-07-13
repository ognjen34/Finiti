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
    public class TermValidationService : ITermValidationService
    {
        private IForbiddenWordRepository _forbiddenWordRepository;

        public TermValidationService(IForbiddenWordRepository forbiddenWordRepository)
        {
            _forbiddenWordRepository = forbiddenWordRepository;
        }

        public Task<bool> IsValidTerm(GlossaryTerm term)
        {

            if (TermHasForbiddenWords(term))
            {
                throw new TermValidationException("Term contains forbidden words.");
            }
            if (!HasEnoughCharacters(term, 30))
            {
                throw new TermValidationException("Term definition must be at least 30 characters long.");
            }
            return Task.FromResult(true);

        }

        private bool HasEnoughCharacters(GlossaryTerm term,int minimumCharacters)
        {
            return term.Definition.Length >= minimumCharacters;
        }

        private bool TermHasForbiddenWords(GlossaryTerm term)
        {
            List<ForbiddenWord> forbiddenWords = _forbiddenWordRepository.GetAll().Result;

            foreach (var forbiddenWord in forbiddenWords)
            {
                if (term.Definition.Contains(forbiddenWord.Word, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
