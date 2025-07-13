using Finiti.DOMAIN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Repositories
{
    public interface IForbiddenWordRepository
    {
        public Task<List<ForbiddenWord>> GetAll();
        public Task<ForbiddenWord> Add(ForbiddenWord forbiddenWord);
        public Task<bool> Delete(int id);
    }
}
