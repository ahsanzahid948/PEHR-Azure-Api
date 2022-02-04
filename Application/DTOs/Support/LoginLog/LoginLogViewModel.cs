using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.LoginLog
{
    public class LoginLogViewModel
    {
        public virtual long LoginLogId { get; set; }
        public virtual string LoginUser { get; set; }
        public virtual DateTime? LoginTime { get; set; }
        public virtual DateTime? LogoutTime { get; set; }
        public virtual string BrowserType { get; set; }
        public virtual string Resolution { get; set; }
        public virtual string DownloadSpeed { get; set; }
        public virtual string UploadSpeed { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual string FrontendVersion { get; set; }
        public virtual string BackendVersion { get; set; }
        public virtual string NPI { get; set; }
    }
}
