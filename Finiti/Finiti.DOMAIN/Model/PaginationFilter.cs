using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DOMAIN.Model
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string TermQuery { get; set; }
        public string AuthorQuery { get; set; }
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
            TermQuery = string.Empty;
            AuthorQuery = string.Empty;

        }
        public PaginationFilter(int pageNumber, int pageSize, string term, string author)
        {
            PageNumber = pageNumber < 1 || pageNumber == null ? 1 : pageNumber;
            PageSize = pageSize > 10 || pageSize == null ? 10 : pageSize;
            TermQuery = term == null ? "" : term;
            AuthorQuery = author == null ? "" : author;

        }
    }
}
