using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DATA.Model
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public virtual RoleEntity Role { get; set; } 
    }
}
