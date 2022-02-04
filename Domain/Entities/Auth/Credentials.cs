using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class Credentials
    {
        public virtual long Seq_Num { get; set; }
        public virtual long Entity_Seq_Num { get; set; }
        public virtual long Default_Entity_Seq_Num { get; set; }
        public virtual string User_Name { get; set; }
        public virtual string Db_User_Name { get; set; }
        public virtual string Db_Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string User_Type { get; set; }
        public virtual string Tns_Name { get; set; }
        public virtual string Redirect_Url { get; set; }
        public virtual long App_User_Seq_Num { get; set; }
        public virtual long? User_Access_Seq_Num { get; set; }
        public virtual string Merchant_Client_Id { get; set; }
        public virtual string Gatway_Id { get; set; }
        public virtual string Trans_First { get; set; }
        public virtual string Doc_Path { get; set; }
        public virtual string Host_User { get; set; }
        public virtual string Host_Password { get; set; }
        public virtual string Host_Name { get; set; }
        public virtual string Emr_Doc_Path { get; set; }
        public virtual string Emr_Docs_Host_Name { get; set; }
        public virtual string Emr_Docs_Host_User { get; set; }
        public virtual string Emr_Docs_Host_Password { get; set; }
        public virtual string Arch_Doc_Path { get; set; }
        public virtual string Arch_Host_Name { get; set; }
        public virtual string Arch_Host_User { get; set; }
        public virtual string Arch_Host_Password { get; set; }
        public virtual string Epcs { get; set; }
        public virtual string Epcs_2nd_Approval { get; set; }
        public virtual string Approve_Epcs { get; set; }
        public virtual string Ipad_Url { get; set; }
        public virtual string Ipad_Version { get; set; }
        public virtual string FaxServer { get; set; }
        public virtual string RxEMRServer { get; set; }
        public virtual string Account_Type { get; set; }
        public virtual string First_Name { get; set; }
        public virtual string Last_Name { get; set; }
        public virtual string Switch_Request_Url { get; set; }
        //public virtual string RXEMRSERVER { get; set; }
        public virtual string Provider_Fee { get; set; }
        public virtual string Flat_Fee { get; set; }
        public virtual string Kiosk_Active { get; set; }
        public virtual string Web_Service_Url { get; set; }
        public virtual string Emr_Url { get; set; }
        public virtual string Company { get; set; }
        public virtual string Company_Seq_Num { get; set; }


    }
}
