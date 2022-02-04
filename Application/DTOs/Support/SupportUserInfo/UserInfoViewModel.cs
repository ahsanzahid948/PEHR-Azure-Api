using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserInfoViewModel : PayLoad
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Active { get; set; }
        public DateTime EntryDate { get; set; }
        public string EnteredBy { get; set; }
        public string Type { get; set; }
        public string FullAccess { get; set; }
        public string ViewOnly { get; set; }
        public long TeamSeqNum { get; set; }
        public string SecretKey { get; set; }
        public string Is2fac { get; set; }
        public string ShowSupport { get; set; }
        public string ShowImplementation { get; set; }
        public string AccountActivated { get; set; }
        public string Token { get; set; }
        public string Enpassword { get; set; }
        public string Team { get; set; }


    }
}