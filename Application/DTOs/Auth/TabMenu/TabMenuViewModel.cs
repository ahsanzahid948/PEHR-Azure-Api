using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.TabMenu
{
    public class TabMenuViewModel
    {
        public virtual long TabId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Icon { get; set; }
        public virtual long? SortOrder { get; set; }
        public virtual string ERX { get; set; }
        public virtual string Admin { get; set; }
    }
}
