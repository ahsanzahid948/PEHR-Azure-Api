using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.EPCSSetting
{
    public class EPCSSettingViewModel
    {
        public virtual long SettingId { get; set; }
        public virtual string Email { get; set; }      
        public virtual string ApproveEpcs { get; set; } 
        public virtual string Licensekey { get; set; } 
        public virtual string TokenRef { get; set; } 
        public virtual string DefaultTokenType { get; set; } 
        public virtual string EpcsRegistration { get; set; }
        public virtual long? ProviderId { get; set; }
    }
}
