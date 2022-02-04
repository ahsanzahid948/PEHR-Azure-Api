using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
   public class SystemAdvOptionsAssignment
    {
        public long Seq_Num { get; set; }
        public long Advance_Options_Seq_Num { get; set; }
        public long Entity_Seq_Num { get; set; }
        public string Visible { get; set; }
        public string Entered_By { get; set; }
        public DateTime Entery_Date { get; set; }
    }
}
