using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string message) : base(message)
        {
        }
        public InvalidPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public InvalidPasswordException() : base("Invalid password provided.")
        {
        }
    }
}
