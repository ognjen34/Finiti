using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Exceptions
{
    public class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException(string message) : base(message)
        {
        }
        public AuthorNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public AuthorNotFoundException() : base("Author not found.")
        {
        }
    }
}
