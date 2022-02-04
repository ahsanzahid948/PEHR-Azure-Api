using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class SystemAdvanceFeatures
    {
        public long Seq_Num { get; set; }
        public long Advance_Options_Seq_Num { get; set; }
        public long Entity_Seq_Num { get; set; }
        public string Visible { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Help_Text { get; set; }
    }
}
