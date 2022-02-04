using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.SSOInfo
{
    public class SSOInfoViewModel
    {
        public virtual long CredentialId { get; set; }
        public virtual string SSOUser { get; set; }
        public virtual string SSOPassword { get; set; }
        public virtual string Email { get; set; }
        public virtual string EnteredBy { get; set; }
        public virtual DateTime? EntryDate { get; set; }
        public virtual string SSOType { get; set; }
        public virtual long? EntityId { get; set; }
    }
}
