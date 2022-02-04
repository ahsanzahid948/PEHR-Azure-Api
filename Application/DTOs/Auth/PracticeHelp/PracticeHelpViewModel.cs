using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.PracticeHelp
{
    public class PracticeHelpViewModel
    {
        public virtual long PracticeHelpId { get; set; }
        public virtual string ButtonId { get; set; }
        public virtual string Url { get; set; }
    }
}
