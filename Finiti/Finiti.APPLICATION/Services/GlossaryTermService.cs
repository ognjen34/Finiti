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
    public class GlossaryTermService : IGlossaryTermService
    {
        private readonly IGlossaryTermRepository _glossaryTermRepository;
        private readonly ITermValidationService _termValidationService;
        public GlossaryTermService(IGlossaryTermRepository glossaryTermRepository, ITermValidationService termValidationService)
        {
            _glossaryTermRepository = glossaryTermRepository;
            _termValidationService = termValidationService;
        }
        public Task<GlossaryTerm> Add(GlossaryTerm glossaryTerm)
        {
            GlossaryTerm addedTerm = _glossaryTermRepository.Add(glossaryTerm).Result;
            return Task.FromResult(addedTerm);

        }

        public Task<GlossaryTerm> Archive(int glossaryTermId)
        {
            GlossaryTerm glossaryTerm = _glossaryTermRepository.GetById(glossaryTermId).Result;
            if (glossaryTerm.Status != GlossaryTermStatus.PUBLISHED)
            {
                throw new TermNotPublishedException("Glossary term must be published before it can be archived.");
            }
            GlossaryTerm archivedTerm = _glossaryTermRepository.Archive(glossaryTermId).Result;
            return Task.FromResult(archivedTerm);
        }

        public Task<GlossaryTerm> GetById(int id)
        {

            GlossaryTerm glossaryTerm = _glossaryTermRepository.GetById(id).Result;  
            return Task.FromResult(glossaryTerm);
        }

        public Task<GlossaryTerm> GetByName(string name)
        {

            GlossaryTerm glossaryTerm = _glossaryTermRepository.GetByName(name).Result;
            return Task.FromResult(glossaryTerm);
        }

        public Task<GlossaryTerm> Publish(int glossaryTermId)
        {
            GlossaryTerm glossaryTerm = _glossaryTermRepository.GetById(glossaryTermId).Result;
            if (_termValidationService.IsValidTerm(glossaryTerm).Result)
            {
                GlossaryTerm publishedTerm = _glossaryTermRepository.Publish(glossaryTermId).Result;
                return Task.FromResult(publishedTerm);
            }
            else
            {
                throw new Exception("Glossary term validation failed.");
            }
        }

        public Task<PaginationReturnObject<GlossaryTerm>> Search(PaginationFilter page)
        {
            PaginationReturnObject<GlossaryTerm> glossaryTerms = _glossaryTermRepository.Search(page).Result;
            return Task.FromResult(glossaryTerms);
        }
    }
}
