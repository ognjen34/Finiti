using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Exceptions
{
    public  class TermValidationException : Exception
    {
        public TermValidationException(string message) : base(message)
        {
        }
        public TermValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    
    
    }
}
