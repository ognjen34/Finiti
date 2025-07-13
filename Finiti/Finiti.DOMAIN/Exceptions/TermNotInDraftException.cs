using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Exceptions
{
    public class TermNotInDraftException : Exception
    {
        public TermNotInDraftException(string message) : base(message)
        {
        }
        public TermNotInDraftException(string message, Exception innerException) : base(message, innerException)
        {
        }
    
    
    }
}
