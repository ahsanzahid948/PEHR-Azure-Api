using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class TabMenu
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Icon { get; set; }
        public virtual long? Sort_Order { get; set; }
        public virtual string For_ERX { get; set; }
        public virtual string For_Admin { get; set; }

    }
}
