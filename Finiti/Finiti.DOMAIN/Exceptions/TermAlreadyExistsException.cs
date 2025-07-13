using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Exceptions
{
    public class TermAlreadyExistsException : Exception
    {
        public TermAlreadyExistsException(string message) : base(message)
        {
        }
        public TermAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public TermAlreadyExistsException() : base("Term already exists.")
        {
        }
    }
}
