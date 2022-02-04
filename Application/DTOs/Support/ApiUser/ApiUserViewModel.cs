using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApiUser
{
    public class ApiUserViewModel
    {
        public virtual long UserId { get; set; }
        public virtual long PatientId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DOB { get; set; }
        public virtual string Gender { get; set; }
        public virtual string ClientId { get; set; }
        public virtual string ProvAllowAccess { get; set; }
        public virtual string PatAllowAccess { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string AccessToken { get; set; }
    }
}
