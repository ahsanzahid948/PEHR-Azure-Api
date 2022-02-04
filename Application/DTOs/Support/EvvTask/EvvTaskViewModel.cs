using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.EvvTask
{
    public class EvvTaskViewModel
    {
        public virtual string TaskId { get; set; }
        public virtual string State { get; set; }
        public virtual string TaskName { get; set; }
    }
}
