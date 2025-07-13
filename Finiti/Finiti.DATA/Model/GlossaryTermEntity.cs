using Finiti.DOMAIN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DATA.Model
{
    public class GlossaryTermEntity
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public virtual AuthorEntity Author { get; set; }
        public GlossaryTermStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
