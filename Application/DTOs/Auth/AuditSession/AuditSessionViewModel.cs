using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.AuditSession
{
    public class AuditSessionViewModel
    {
        public long AuditSessionId { get; set; }
        public string Url { get; set; }
        public string IpAddress { get; set; }
        public string AppUser { get; set; }
        public string DbUser { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public DateTime? EntryDate { get; set; }
        public string EnteredBy { get; set; }
    }
}
