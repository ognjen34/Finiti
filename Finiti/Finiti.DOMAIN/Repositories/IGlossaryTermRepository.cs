using Finiti.DOMAIN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Repositories
{
    public interface IGlossaryTermRepository
    {
        Task<GlossaryTerm> GetById(int id);
        Task<GlossaryTerm> GetByName(string name);
        Task<GlossaryTerm> Add(GlossaryTerm glossaryTerm);
        Task<PaginationReturnObject<GlossaryTerm>> Search(PaginationFilter page);
        Task<PaginationReturnObject<GlossaryTerm>> GetAuthorsTerms(PaginationFilter page, int authorId);
        Task<GlossaryTerm> Publish(int glossaryTermId);
        Task<GlossaryTerm> Archive(int glossaryTermId);
        Task<GlossaryTerm> Delete(int glossaryTermId);
        Task<GlossaryTerm> Update(GlossaryTerm glossaryTerm);
    }
}
