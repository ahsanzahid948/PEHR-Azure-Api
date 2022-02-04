using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.PracticeSetup
{
    public class PracticeSetupViewModel
    {
        public virtual long PracticeSetupId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string AutoStatus { get; set; }
        public virtual long? EntityId { get; set; }
        public virtual string Status { get; set; }
        public virtual string SupportMessage { get; set; }
    }
}
