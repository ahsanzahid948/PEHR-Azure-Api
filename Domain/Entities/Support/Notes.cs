using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Notes
{
    public class Notes
    {
        public long Seq_Num { get; set; }
        public DateTime Note_Date { get; set; }
        public string Type { get; set; }
        public string Comments { get; set; }
        public long Entity_Seq_Num { get; set; }
        public byte[] Attached_File { get; set; }
        public string Resolved { get; set; }
        public string Assigned_To { get; set; }
        public DateTime Entry_Date { get; set; }
        public string Entered_By { get; set; }
        public DateTime From_Time { get; set; }
        public DateTime To_Time { get; set; }

    }
}
