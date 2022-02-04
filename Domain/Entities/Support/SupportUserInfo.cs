using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Entities
{
    public class SupportUserInfo 
    {
        public long Seq_Num { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Active { get; set; }
        public DateTime Entry_Date { get; set; }
        public string Entered_By { get; set; }
        public string Type { get; set; }
        public string Full_Access { get; set; }
        public string View_Only { get; set; }
        public long Team_Seq_Num { get; set; }
        public string Secret_Key { get; set; }
        public string Is_2Fac { get; set; }
        public string Show_Support { get; set; }
        public string Show_Implementation { get; set; }
        public string Account_Activated { get; set; }
        public string Token { get; set; }
        public string En_Password { get; set; }
        public long Team { get; set; }
    }
}
