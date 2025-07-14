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
    public class GlossaryTermRepository : IGlossaryTermRepository
    {
        private readonly IMapper _mapper;
        private readonly DbSet<GlossaryTermEntity> _glossaryTerms;
        private readonly DatabaseContext _context;
        public GlossaryTermRepository(DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _glossaryTerms = context.GlossaryTerms;
            _context = context;
        }
        public Task<GlossaryTerm> Add(GlossaryTerm glossaryTerm)
        {
            GlossaryTermEntity existingTerm = _glossaryTerms.FirstOrDefault(gt => gt.Term == glossaryTerm.Term && gt.IsDeleted == false && gt.Status == GlossaryTermStatus.PUBLISHED);
            if (existingTerm != null)
            {
                throw new TermAlreadyExistsException("Glossary term with this name already exists.");
            }
            GlossaryTermEntity glossaryTermEntity = _mapper.Map<GlossaryTermEntity>(glossaryTerm);
            glossaryTermEntity.Author = null;
            glossaryTermEntity.AuthorId = glossaryTerm.Author.Id; 
            glossaryTermEntity.Status = GlossaryTermStatus.DRAFT; 
            _glossaryTerms.Add(glossaryTermEntity);
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<GlossaryTerm>(glossaryTermEntity));

        }

        public Task<GlossaryTerm> Archive(int glossaryTermId)
        {
            GlossaryTermEntity glossaryTermEntity = _glossaryTerms.FirstOrDefault(gt => gt.Id == glossaryTermId && gt.IsDeleted == false);
            if (glossaryTermEntity == null)
            {
                throw new ResourceNotFoundException("Glossary term not found with the provided ID.");
            }
            glossaryTermEntity.Status = GlossaryTermStatus.ARCHIVED;
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<GlossaryTerm>(glossaryTermEntity));

        }

        public Task<GlossaryTerm> Delete(int glossaryTermId)
        {
            GlossaryTermEntity glossaryTermEntity = _glossaryTerms.FirstOrDefault(gt => gt.Id == glossaryTermId && gt.IsDeleted == false);
            if (glossaryTermEntity == null)
            {
                throw new ResourceNotFoundException("Glossary term not found with the provided ID.");
            }
            glossaryTermEntity.IsDeleted = true;
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<GlossaryTerm>(glossaryTermEntity));

        }

        public Task<GlossaryTerm> GetById(int id)
        {

            GlossaryTermEntity glossaryTermEntity = _glossaryTerms.FirstOrDefault(gt => gt.Id == id && gt.IsDeleted == false);
            if (glossaryTermEntity == null)
            {
                throw new ResourceNotFoundException("Glossary term not found with the provided ID.");
            }
            return Task.FromResult(_mapper.Map<GlossaryTerm>(glossaryTermEntity));
        }

        public Task<GlossaryTerm> GetByName(string name)
        {
            GlossaryTermEntity glossaryTermEntity = _glossaryTerms.FirstOrDefault(gt => gt.Term == name && gt.IsDeleted == false);
            if (glossaryTermEntity == null)
            {
                throw new ResourceNotFoundException("Glossary term not found with the provided term.");
            }
            return Task.FromResult(_mapper.Map<GlossaryTerm>(glossaryTermEntity));
        }

        public Task<GlossaryTerm> Publish(int glossaryTermId)
        {
            GlossaryTermEntity glossaryTermEntity = _glossaryTerms.FirstOrDefault(gt => gt.Id == glossaryTermId && gt.IsDeleted == false);
            if (glossaryTermEntity == null)
            {
                throw new ResourceNotFoundException("Glossary term not found with the provided ID.");
            }
            if (glossaryTermEntity.Status == GlossaryTermStatus.PUBLISHED)
            {
                throw new InvalidOperationException("Glossary term is already published.");
            }
            glossaryTermEntity.Status = GlossaryTermStatus.PUBLISHED;
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<GlossaryTerm>(glossaryTermEntity));

        }
        public Task<PaginationReturnObject<GlossaryTerm>> GetAuthorsTerms(PaginationFilter page,int authorId)
        {
            IQueryable<GlossaryTermEntity> query = _glossaryTerms;

            query = query.Where(gt => gt.IsDeleted == false);

            query = query.Where(gt => gt.AuthorId == authorId);

            if (!string.IsNullOrEmpty(page.TermQuery))
            {
                query = query.Where(gt => gt.Term.ToLower().Contains(page.TermQuery.ToLower()));
            }

            if (!string.IsNullOrEmpty(page.AuthorQuery))
            {

                query = query.Where(gt => gt.Author.FirstName.ToLower().Contains(page.AuthorQuery.ToLower()) || gt.Author.LastName.ToLower().Contains(page.AuthorQuery.ToLower()));

            }


            int totalCount = query.Count();

            List<GlossaryTermEntity> glossaryTerms = query
                 .OrderBy(gt => gt.Term)
                .Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize)
                .ToList();

            PaginationReturnObject<GlossaryTerm> result = new PaginationReturnObject<GlossaryTerm>(_mapper.Map<IEnumerable<GlossaryTerm>>(glossaryTerms), page.PageNumber, page.PageSize, totalCount);

            return Task.FromResult(result);

        }

        public Task<PaginationReturnObject<GlossaryTerm>> Search(PaginationFilter page)
        {
            IQueryable<GlossaryTermEntity> query = _glossaryTerms;

            query = query.Where(gt => gt.IsDeleted == false);

            query = query.Where(gt => gt.Status == GlossaryTermStatus.PUBLISHED);

            if (!string.IsNullOrEmpty(page.TermQuery))
            {
                query = query.Where(gt => gt.Term.ToLower().Contains(page.TermQuery.ToLower()));
            }

            if (!string.IsNullOrEmpty(page.AuthorQuery))
            {
                
                    query = query.Where(gt => gt.Author.FirstName.ToLower().Contains(page.AuthorQuery.ToLower()) || gt.Author.LastName.ToLower().Contains(page.AuthorQuery.ToLower()));
                
            }
           

            int totalCount = query.Count();

            List<GlossaryTermEntity> glossaryTerms = query
                 .OrderBy(gt => gt.Term)
                .Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize)
                .ToList();

            PaginationReturnObject<GlossaryTerm> result = new PaginationReturnObject<GlossaryTerm>(_mapper.Map<IEnumerable<GlossaryTerm>>(glossaryTerms), page.PageNumber, page.PageSize, totalCount);

            return Task.FromResult(result);

        }

        public Task<GlossaryTerm> Update(GlossaryTerm glossaryTerm)
        {
            GlossaryTermEntity glossaryTermEntity = _glossaryTerms.FirstOrDefault(gt => gt.Id == glossaryTerm.Id && gt.IsDeleted == false);
            if (glossaryTermEntity == null)
            {
                throw new ResourceNotFoundException("Glossary term not found with the provided ID.");
            }
            if (glossaryTermEntity.Status != GlossaryTermStatus.DRAFT)
            {
                throw new TermNotInDraftException("Glossary term must be in draft status to be updated.");
            }
            glossaryTermEntity.Term = glossaryTerm.Term;
            glossaryTermEntity.Definition = glossaryTerm.Definition;
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<GlossaryTerm>(glossaryTermEntity));

        }
    }
}
