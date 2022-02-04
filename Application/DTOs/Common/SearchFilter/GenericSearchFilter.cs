using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Common.SearchFilter
{
    public class GenericSearchFilter
    {
        public string sort_by;
        public string order_by;
        public int page_no;
        public int min_limit;
        public int max_limit;
    }
}
