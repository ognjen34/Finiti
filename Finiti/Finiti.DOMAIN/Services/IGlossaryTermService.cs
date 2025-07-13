using Finiti.DOMAIN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Services
{
    public interface IGlossaryTermService
    {
        Task<GlossaryTerm> GetById(int id);
        Task<GlossaryTerm> GetByName(string name);
        Task<GlossaryTerm> Add(GlossaryTerm glossaryTerm);
        Task<PaginationReturnObject<GlossaryTerm>> Search(PaginationFilter page);
        Task<GlossaryTerm> Publish(int glossaryTermId);
        Task<GlossaryTerm> Archive(int glossaryTermId);
    }
}
