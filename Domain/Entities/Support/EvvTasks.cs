using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class EvvTasks
    {
        public virtual string Task_Id { get; set; }
        public virtual string State { get; set; }
        public virtual string TaskName { get; set; }
    }
}
