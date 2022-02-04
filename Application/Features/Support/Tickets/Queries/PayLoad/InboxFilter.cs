using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Ticket
{
    public class InboxFilter : PayLoad
    {
        public string SortName { get; set; }
        public string SortOrder { get; set; }
        public string Query { get; set; }
        public string Qtype { get; set; }

    }
}
