using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Model
{
    public class GlossaryTerm
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public DateTime CreatedAt { get; set; }
        public GlossaryTermStatus Status { get; set; }
        public Author Author { get; set; }

    }

    public enum GlossaryTermStatus
    {
        DRAFT,
        PUBLISHED,
        ARCHIVED
    }
}
