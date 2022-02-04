using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
   public class ApiUser
    {
        public virtual long Seq_Num { get; set; }
        public virtual long Patient_Seq_Num { get; set; }
        public virtual string First_Name { get; set; }
        public virtual string Last_Name { get; set; }
        public virtual DateTime Dob { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Client_Id { get; set; }
        public virtual string Prov_Allow_Access { get; set; }
        public virtual string Pat_Allow_Access { get; set; }
        public virtual string User_Name { get; set; }
        public virtual string Password { get; set; }
        public virtual string Access_Token { get; set; }

    }
}
