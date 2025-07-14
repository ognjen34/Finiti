using Finiti.DOMAIN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Services
{
    public interface ITermValidationService
    {
        public Task<bool> IsValidTerm(GlossaryTerm term);
    }
}
