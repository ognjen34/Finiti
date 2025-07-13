using Finiti.DOMAIN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Services
{
    public interface IForbiddenWordService
    {
        public Task<ForbiddenWord> Add(ForbiddenWord forbiddenWord);
        public Task<bool> DeleteByWord(string word);
    }
}
