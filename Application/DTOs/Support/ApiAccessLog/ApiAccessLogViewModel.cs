using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApiAccessLog
{
   public class ApiAccessLogViewModel
    {
        public virtual long Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string DataAccessed { get; set; }
        public DateTime AccessedDate { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual string AccessToken { get; set; }
    }
}
