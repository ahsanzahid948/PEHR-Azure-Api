using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class PracticeHelp
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Button_Id { get; set; }
        public virtual string Url { get; set; }
    }
}
