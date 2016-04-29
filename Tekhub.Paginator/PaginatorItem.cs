using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekhub.Paginator
{
    public class PaginatorItem
    {
        public PaginatorItemType PaginatorItemType { get; set; }
        public int Page { get; set; }
        public bool IsCurrentPage { get; set; }
    }
}
