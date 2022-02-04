using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class Menu
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Icon { get; set; }
        public virtual long? Sort_Order { get; set; }
        public virtual long? Tab_Menu_Seq_Num { get; set; }
        public virtual string Category_Type { get; set; }
        public virtual string Visible { get; set; }


    }
}
