using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class DataExportSetting
    {
        public virtual long Seq_Num { get; set; }
        public virtual long? User_Seq_Num { get; set; }
        public virtual long? Auth_Entity_Seq_Num { get; set; }
        public virtual DateTime? Date_From { get; set; }
        public virtual DateTime? Date_To { get; set; }
        public virtual string File_Location_Type { get; set; }
        public virtual string Ftp_Url { get; set; }
        public virtual string FTP_Port { get; set; }
        public virtual string FTP_User { get; set; }
        public virtual string FTP_Pas { get; set; }
        public virtual string Email { get; set; }
        public virtual string Export_Type_Flag { get; set; }
        public virtual string Relative_Export_Type { get; set; }
        public virtual long? Relative_Export_Value { get; set; }
        public virtual DateTime? Specific_Date { get; set; }
        public virtual string Export_Time { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entry_Date { get; set; }
        public virtual string Account_Num { get; set; }
        public virtual string All_Patient_Flag { get; set; }
        public virtual string Status { get; set; }

    }
}
