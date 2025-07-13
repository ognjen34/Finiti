using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Exceptions
{
    public class TermNotPublishedException : Exception
    {
        public TermNotPublishedException(string message) : base(message)
        {
        }
        public TermNotPublishedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
