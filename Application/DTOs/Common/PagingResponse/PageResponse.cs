using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Common.PagingResponse
{
    public class PageResponse<T>
    {
        public int Total { get; set; }
        public List<T> Rows { get; set; }

        public PageResponse(List<T> rows, int total)
        {
            Rows = rows;
            Total = total;
        }
    }
}
