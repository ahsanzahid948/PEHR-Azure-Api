using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Ticket
{
    public class SearchFilter : PayLoad
    {
        public string Page { get; set; }
        public string Rp { get; set; }
        public string Sortname { get; set; }
        public string Sortorder { get; set; }
        public string Query { get; set; }
        public string Qtype { get; set; }
        public string TxtQuickSearch { get; set; }
    }
}
