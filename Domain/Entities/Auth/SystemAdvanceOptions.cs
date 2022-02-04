using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class SystemAdvanceOptions
    {
        public long Seq_Num { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Help_Text { get; set; }
    }
}
