using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Exceptions
{
    public class AuthorAlreadyExistsException :Exception
    {
        
        public AuthorAlreadyExistsException(string message)
            : base(message)
        {
        }
        public AuthorAlreadyExistsException()
           : base("Author Already Exists Exceptions")
        {
        }
    }

}
