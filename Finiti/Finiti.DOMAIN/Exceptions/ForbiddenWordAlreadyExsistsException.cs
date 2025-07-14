using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Exceptions
{
    public class ForbiddenWordAlreadyExsistsException : Exception
    {
        public ForbiddenWordAlreadyExsistsException(string message) : base(message)
        {
        }
        public ForbiddenWordAlreadyExsistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
